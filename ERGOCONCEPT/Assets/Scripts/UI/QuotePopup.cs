using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Text.RegularExpressions;

public class QuotePopup : MonoBehaviour
{
    public Text ChairName;
    public Text PriceTTC;
    public Text PriceSecu;
    public Text Errors;
    public InputField NameField;
    public InputField PhoneField;
    public InputField EmailField;
    public InputField CodePostalField;
    public SettingsManager SettingsManager;
    public UnityEvent OnSendEmailSuccess;
    public Transform model3D;
    public float modelRotation;

    public const string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
        + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";


    private void OnEnable()
    {
        model3D.localEulerAngles = new Vector3(0, modelRotation, 0);
        PriceTTC.text = SettingsManager.GetPriceTTC();
        PriceSecu.text = SettingsManager.GetPriceLPP();
        ChairName.text = "Votre " + SettingsManager.ChairName.text;
    }

    public void SendEmail()
    {
        if (!CheckErrors())
        {
            return;
        }

        string email = EmailField.text;
        string subject = "Demande de devis " + SettingsManager.ChairName.text;
        string body = "Bonjour,\r\n\r\nJ'aimerai être contacté à propos du fauteuil " + SettingsManager.ChairName.text + "\r\n\r\n";

        body += "Accessoires/options : \r\n";
        foreach (AccessoryToggle accessory in SettingsManager.AccessorySelected)
        {
            body += "- " + accessory.Label.text + "   référence : " + accessory.Reference + "\r\n";
        }

        body += "\r\n";
        body += "Mon téléphone : " + PhoneField.text + "\r\n";
        body += "Cordialement,\r\n";
        body += NameField.text;

        body = MyEscapeURL(body);
        subject = MyEscapeURL(subject);

        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);

        OnSendEmailSuccess.Invoke();
    }

    private string MyEscapeURL(string URL)
    {
        return WWW.EscapeURL(URL).Replace("+", "%20");
    }

    private bool CheckErrors()
    {
        if (NameField.text.Length < 2)
        {
            Errors.text = "Nom incorrect";
            return false;
        }

        if (PhoneField.text.Length < 10)
        {
            Errors.text = "Téléphone incorrect";
            return false;
        }

        if (CodePostalField.text.Length < 5)
        {
            Errors.text = "Code postal incorrect";
            return false;
        }

        if (!validateEmail(EmailField.text))
        {
            Errors.text = "Email incorrect";
            return false;
        }

        return true;
    }

    public static bool validateEmail(string email)
    {
        if (email != null)
            return Regex.IsMatch(email, MatchEmailPattern);
        else
            return false;
    }
}