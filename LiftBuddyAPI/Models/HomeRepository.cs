using DataAccess;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace LiftBuddyAPI.Models
{
    public class HomeRepository
    {
        Random random = new Random();
        public async Task<UserLoginResponse> Login(string MobileNo)
        {
            UserLoginResponse objResponse = new UserLoginResponse();
            try
            {
                using (var entities = new LiftBuddyEntities())
                {
                    var tblUser = new tblUser();
                    tblUser = entities.tblUsers.FirstOrDefault(e => e.MobileNo == MobileNo);
                    if (tblUser == null)
                    {
                        objResponse.IsRegistered = "No";
                        tblUser = new tblUser();
                        tblUser.MobileNo = MobileNo;
                        tblUser.CreatedDate = DateTime.Now;
                        tblUser.IsActive = true;
                        entities.tblUsers.Add(tblUser);

                        await entities.SaveChangesAsync();
                    }
                    else
                    {
                        objResponse.IsRegistered = "Yes";
                    }
                    string OTP = await SendOTP(MobileNo);
                    if (!string.IsNullOrEmpty(OTP))
                    {
                        SaveOTP(OTP, MobileNo);
                        objResponse.Status = true;
                        objResponse.UserId = tblUser.ID;                 
                        objResponse.ResponseValue = "Otp Send Successfully";
                    }
                    else
                    {
                        objResponse.Status = true;
                        objResponse.ResponseValue = "Unsuccessfull";
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.Status = true;
                objResponse.ResponseValue = ex.Message;
            }
            return objResponse;
        }


        public async Task<UserResponse> VerifyOTP(tblOtpTransaction objOTP)
        {
            UserResponse objResponse = new UserResponse();
            try
            {
                tblOtpTransaction verifyWith = new tblOtpTransaction();
                using (var entities = new LiftBuddyEntities())
                {
                    verifyWith = await Task.Run(() => entities.tblOtpTransactions.Where(o => o.MobileNo == objOTP.MobileNo).OrderByDescending(o => o.GenerateDateTime).FirstOrDefault());                
                    if (objOTP.OTP == verifyWith.OTP)
                    {
                        
                        verifyWith.IsUsed = true;
                        verifyWith.UsedOn = DateTime.Now;
                        await entities.SaveChangesAsync();
                        objResponse.Status = true;
                        objResponse.UserId = await Task.Run(() => entities.tblUsers.Where(o => o.MobileNo == objOTP.MobileNo).Select(o => o.ID).FirstOrDefault());
                        objResponse.ResponseValue = "OTP matched.";
                    }
                    else
                    {
                        objResponse.Status = false;
                        objResponse.ResponseValue = "Otp mismatched.";
                        objResponse.UserId = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.Status = false;
                objResponse.ResponseValue = ex.Message;
            }
            return objResponse;
        }

        public async Task<UserResponse> GetUser(string MobileNo)
        {
            UserResponse objResponse = new UserResponse();
            try
            {
                using (var entities = new LiftBuddyEntities())
                {
                    var tblUser = new tblUser();
                    tblUser =  await Task.Run(() => entities.tblUsers.FirstOrDefault(s => s.MobileNo == MobileNo));
                    if (tblUser != null)
                    {
                        tblUser.UserId = tblUser.ID;
                        objResponse.UserId = tblUser.ID;
                        if (tblUser.Dob != null)
                        {
                            tblUser.DobStr =  String.Format("{0:dd MMMM yyyy}", tblUser.Dob);
                        }
                        if (!string.IsNullOrEmpty(tblUser.ProfilePic))
                        {
                            var serverPath = HttpContext.Current.Server.MapPath("~/UploadedImage");
                            var path = Path.Combine(serverPath, tblUser.ProfilePic);
                            tblUser.ProfilePic = "http://" + System.Web.HttpContext.Current.Request.Url.Host + "/UploadedImage/" + tblUser.ProfilePic;
                            tblUser.fileBytes = ImageToBase64(path);
                        }
                        objResponse.Status = true;
                        var serializer = new JavaScriptSerializer() { MaxJsonLength = 2147483647 };                        
                        objResponse.ResponseValue = serializer.Serialize(tblUser);
                    }
                    else
                    {
                        objResponse.Status = false;
                        objResponse.ResponseValue = "User with Mobile no not found.";
                    }
                }
             }
            catch (Exception ex)
            {
                objResponse.Status = false;
                objResponse.ResponseValue = ex.Message;
            }
            return objResponse;
        }

        private bool SaveOTP(string OTP, string MobileNo)
        {
            tblOtpTransaction objOtp = new tblOtpTransaction();
            objOtp.GenerateDateTime = DateTime.Now;
            objOtp.OTP = OTP;
            objOtp.MobileNo = MobileNo;
            using (var entities = new LiftBuddyEntities())
            {
                try
                {
                    entities.tblOtpTransactions.Add(objOtp);
                    entities.SaveChanges();
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
            return true;
        }

        public async Task<UserResponse> SaveProfileDetail(tblUser detail)
        {
            var responseDetail = new UserResponse();
            var fileExtension = string.Empty;
            var fileName = string.Empty;
            var tblUser = new tblUser();
            string url = string.Empty;
            if (!string.IsNullOrEmpty(detail.fileName))
            {
                if (!detail.fileName.Contains("."))
                {
                    responseDetail.ResponseValue = "Invalid File Name. Please send file name with extension.";
                }
                else
                {
                    var fileExt = detail.fileName.Split('.');
                    if (fileExt.Length <= 0 || fileExt.Length > 2)
                    {
                        responseDetail.ResponseValue = "Invalid File Name.";
                    }
                    else
                    {
                        fileName = fileExt[0];
                        fileExtension = fileExt[1];
                    }
                }
                if (!string.IsNullOrEmpty(fileExtension))
                {
                    if (detail == null || string.IsNullOrEmpty(detail.fileBytes) || string.IsNullOrEmpty(detail.fileName) || string.IsNullOrEmpty(detail.fileBytes))
                    {
                        responseDetail.ResponseValue = "Please send complete image detail.";
                    }
                    else
                    {
                        url = UploadImageCode(detail.fileBytes, fileName, fileExtension);                      
                    }
                }
            }

            try
            {               
                    using (var entities = new LiftBuddyEntities())
                    {
                        tblUser = entities.tblUsers.FirstOrDefault(e => e.MobileNo == detail.MobileNo);
                        if (tblUser != null)
                        {
                        if (!string.IsNullOrEmpty(url))
                        {
                            tblUser.ProfilePic = url;
                        }
                        if (!string.IsNullOrEmpty(detail.Name))
                        {
                            tblUser.Name = detail.Name;
                            var username = detail.Name.Replace(' ', '_') + random.Next(0, 999999).ToString("D6");
                            tblUser.UserName = username;
                        }
                            
                            responseDetail.UserId = tblUser.ID;
                        }
                        await entities.SaveChangesAsync();
                    }
                    responseDetail.ResponseValue = "Details updated successfully.";                   
                    responseDetail.Status = true;                
            }
            catch (Exception e)
            {
                responseDetail.ResponseValue = e.Message;
            }

            return responseDetail;
        }     

        public static string UploadImageCode(string fileBytes, string fileName, string fileExtension)
        {
            byte[] fileBytesArray = Convert.FromBase64String(fileBytes);

            string myfile = fileName + Guid.NewGuid().ToString().Substring(0, 8) + "." + fileExtension;

            var serverPath = HttpContext.Current.Server.MapPath("~/UploadedImage");

            if (!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }

            var path = Path.Combine(serverPath, myfile);
            File.WriteAllBytes(path, fileBytesArray);

            string currentURL = myfile;

            var url = Uri.EscapeUriString(currentURL);
            return url;
        }

        public async Task<string> SendOTP(string mobileNo)
        {
            string OTPGenerated = string.Empty;
            string _numbers = "0123456789";
            Random random = new Random();
            System.Text.StringBuilder builder = new System.Text.StringBuilder(6);
            try
            {
                for (var i = 0; i < 6; i++)
                {
                    builder.Append(_numbers[random.Next(0, _numbers.Length)]);
                }
                OTPGenerated = builder.ToString();
                string message = "<%23>Use " + OTPGenerated + " as one time password to login to your FitBuddy account. /c+XAtKCim0";               
                //string message = "Use " + OTPGenerated + " as one time password to login to your FitBuddy account.";
                var flag = await SendMessage(mobileNo, message);
                if (flag)
                {
                    return await Task.FromResult(OTPGenerated);
                }
                else
                {
                    return await Task.FromResult("");
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult("");
        }

        public async Task<bool> SendMessage(string mobileNo, string MessageBody)
        {    
            var msgUrl = "http://www.apiconnecto.com/API/SMSHttp.aspx?UserId=fitbuddy&pwd=pwd2019&Message={MessageBody}&Contacts={ContactsValue}&SenderId=FITBUD";

            msgUrl = msgUrl.Replace("{MessageBody}", MessageBody).Replace("{ContactsValue}", mobileNo);
            try
            {
                using (var httpClient = new HttpClient())
                {

                    var httpResponse = await httpClient.GetAsync(msgUrl);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        if (httpResponse.Content != null)
                        {
                            var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        }
                        return await Task.FromResult(true);
                        
                    }
                    else
                    {

                        return await Task.FromResult(false);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult(false);
        }

        public async Task<Response> UpdateUserProfile(tblUser detail)
        {
            Response responseDetail = new Response();
            try
            {
                using (var entities = new LiftBuddyEntities())
                {
                    var tblUser = new tblUser();
                    tblUser = await Task.Run(() => entities.tblUsers.FirstOrDefault(s => s.ID == detail.UserId));
                    if (tblUser != null)
                    {

                        var fileExtension = string.Empty;
                        var fileName = string.Empty;                       
                        string url = string.Empty;
                        bool ImageError = false;
                        if (detail.RemoveProfilePic == true)
                        {
                            tblUser.ProfilePic = "";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(detail.fileName))
                            {
                                if (!detail.fileName.Contains("."))
                                {
                                    ImageError = true;
                                    responseDetail.ResponseValue = "Invalid File Name. Please send file name with extension.";
                                }
                                else
                                {
                                    var fileExt = detail.fileName.Split('.');
                                    if (fileExt.Length <= 0 || fileExt.Length > 2)
                                    {
                                        ImageError = true;
                                        responseDetail.ResponseValue = "Invalid File Name.";
                                    }
                                    else
                                    {
                                        fileName = fileExt[0];
                                        fileExtension = fileExt[1];
                                    }
                                }
                                if (!string.IsNullOrEmpty(fileExtension))
                                {
                                    if (detail == null || string.IsNullOrEmpty(detail.fileBytes) || string.IsNullOrEmpty(detail.fileName) || string.IsNullOrEmpty(detail.fileBytes))
                                    {
                                        ImageError = true;
                                        responseDetail.ResponseValue = "Please send complete image detail.";
                                    }
                                    else
                                    {
                                        url = UploadImageCode(detail.fileBytes, fileName, fileExtension);
                                        tblUser.ProfilePic = url;
                                    }
                                }
                                else
                                {
                                    responseDetail.ResponseValue = "Invalid File Name. Please send file name with extension.";
                                    ImageError = true;
                                }
                            }
                        }

                        if (!ImageError)
                        {
                            if (!string.IsNullOrEmpty(detail.DobStr))
                            {
                                DateTime Dob = new DateTime();
                                CultureInfo provider = new CultureInfo("en-GB");
                                Dob = DateTime.Parse(detail.DobStr, provider, DateTimeStyles.NoCurrentDateDefault);
                                tblUser.Dob = Dob;
                            }

                            tblUser.Height = detail.Height;
                            tblUser.Weight = detail.Weight;
                            tblUser.Gender = detail.Gender;
                            tblUser.HeightIn = detail.HeightIn;
                            tblUser.WeightIn = detail.WeightIn;
                            tblUser.UserName = detail.UserName;
                            tblUser.Name = detail.Name;

                            await entities.SaveChangesAsync();
                            responseDetail.Status = true;
                            responseDetail.ResponseValue = "Updated Successfully";
                        }
                    }
                    else
                    {
                        responseDetail.Status = false;
                        responseDetail.ResponseValue = "User not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                responseDetail.Status = false;
                responseDetail.ResponseValue = ex.Message;
            }
            return responseDetail;
        }

        public string ImageToBase64(string path)
        {            
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        
        public async Task<Response> CheckUNValidity(int userId,string UserName)
        {
            Response responseDetail = new Response();
            try
            {
                using (var entities = new LiftBuddyEntities())
                {
                    var user = await Task.Run(() => entities.tblUsers.FirstOrDefault(e => e.ID!=userId && e.UserName == UserName));
                    if (user != null)
                    {                       
                        responseDetail.ResponseValue ="UserName already exist.";
                        responseDetail.Status = true;
                    }
                    else
                    {
                        responseDetail.ResponseValue = "Valid";
                        responseDetail.Status = true;
                    }
                }
            }
            catch (Exception e)
            {
                responseDetail.ResponseValue = e.Message;
                responseDetail.Status = false;
            }
            return responseDetail;
        }

    }
}