using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegratedMessages;

public partial class RecoverAccount : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        btnRecoverPassword.Click+=new EventHandler(btnRecoverPassword_Click);
        btnProceed.Click+=new EventHandler(btnProceed_Click);
        btnChangePassword.Click += new EventHandler(btnChangePassword_Click);
    }

    void btnChangePassword_Click(object sender, EventArgs e)
    {
        Users.ChangePassword(txtAAN.Text.Trim(), txtNewPassword.Text);
        msgAreaPassword.Visible = true;
        msgAreaPassword.InnerText = "Your password has been reset successfully, please login with your new password.";
    }

    void btnRecoverPassword_Click(object sender, EventArgs e)
    {
        var aan = txtAAN.Text.Trim();
        var application = AllotementApplications.GetApplicationsByAAn(aan).FirstOrDefault();

        if (application != null && !string.IsNullOrEmpty(application.ContactNumber))
        {
            OTP.DeleteAllInvalidOTPs(aan); //delete all invalid otps
            var existingOTP = OTP.GetOTP(aan);
            if (existingOTP != null)
            {
                ShowMessage("An OTP has already been sent, please wait for 10 minutes to generate another OTP.");
                return;
            }
            else
            {
                Random ran = new Random();
                var randomOTP = ran.Next(10000, 99999);

                new IntegratedMessageSender().SendMessage("RECOVER_PASSWORD", randomOTP.ToString(), "9041863630");

                OTP.SaveOTP(aan, application.ContactNumber, randomOTP.ToString());
                litPhoneNumber.Text = application.ContactNumber;
                pnlInfoOTP.Visible = true;
                pnlEnterInfo.Visible = false;
            }

        }
        else
        {
            ShowMessage("Invalid information. Either AAN or phone number is invalid.");
        }

    }

    private void ShowMessage(string message)
    {
        msgAreaAan.Visible = true;
        msgAreaAan.InnerText = message;
    }

    private void ShowMessageAboutOTP(string message) {
        msgAreaOtp.Visible = true;
        msgAreaOtp.InnerText = message;
    }


    void btnProceed_Click(object sender, EventArgs e)
    {
        var otp = txtOTP.Text.Trim();
        var aan = txtAAN.Text.Trim();
        bool isOTPValid = OTP.IsOTPValid(otp,aan);

        if (isOTPValid)
        {
            pnlInfoOTP.Visible = false;
            pnlChangePassword.Visible = true;
            msgAreaOtp.Visible = false;
        }
        else
            ShowMessageAboutOTP("Please enter valid OTP");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

}