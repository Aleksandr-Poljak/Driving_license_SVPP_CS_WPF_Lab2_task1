using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SVPP_CS_WPF_Lab2_task1_Driving_license
{
    enum GenderEnum { male, female, other };
    enum EyesEnum { Blue, Brown, Green, Amber, Red, Black };

    /// <summary>
    /// Driver class.
    /// </summary>
    class Driver
    {
        private const string DEFAULT_NAME_SURNAME = "Input name";
        private const int DEFAULT_NUMBER = 0;
        private const string DEFAULT_ADRESS = "Input Adress";
        private const string DEFAULT_PHOTO_PATH = "photo\\default_photo.jpg";
        private const int DEFAULT_HGT = 119;
        private static readonly DateTime DEFAULT_DOB =new DateTime(1989, 1, 1);
        private static readonly DateTime DEFAULT_ISS = DEFAULT_DOB;
        private static readonly DateTime DEFAULT_EXP = DEFAULT_DOB;
        private const char DEFAULT_CLASS_LICENSE = 'Z';


        public string NameSurname { get; set; }
        public int Number { get; set; }
        public string Adress { get; set; }
        public DateTime DOB { get; set; }
        public char Class_License { get; set; } 
        public DateTime ISS { get; set; }
        public DateTime EXP { get; set; }
        public bool OrganDonor { get; set; }
        public string PhotoPath { get; set; }
        public GenderEnum Gender { get; set; }
        public EyesEnum Eyes { get; set; } 
        public int HGT { get; set; }

        
        public Driver()
        {
            NameSurname = DEFAULT_NAME_SURNAME;
            Number = DEFAULT_NUMBER;
            Adress = DEFAULT_ADRESS;
            DOB = DEFAULT_DOB;
            OrganDonor = false;
            PhotoPath = DEFAULT_PHOTO_PATH;
            Gender = GenderEnum.other;
            Eyes = EyesEnum.Amber;
            HGT = DEFAULT_HGT;

        }

        public Driver(string nameSurname, int number, string adress, DateTime dOB, 
            char class_License, DateTime iSS, DateTime eXP, bool organDonor,
            string photoPath, GenderEnum gender, EyesEnum eyes, int hGT)
        {
            NameSurname = nameSurname;
            Number = number;
            Adress = adress;
            DOB = dOB;
            Class_License = class_License;
            ISS = iSS;
            EXP = eXP;
            OrganDonor = organDonor;
            PhotoPath = photoPath;
            Gender = gender;
            Eyes = eyes;
            HGT = hGT;
        }

        public override string ToString()
        {
            string dr = $"FIO: {NameSurname}\nLicense Number: {Number}\nLicense class {Class_License}\nAdress: {Adress}\nDOB: {DOB.ToShortDateString()}\nOrgan donor: {OrganDonor}\n" +
                $"Photo: {PhotoPath}\n Gender: {Gender.ToString()}\nEyes: {Eyes.ToString()}\nHGT: {HGT}\n ISS: {ISS.ToShortDateString()}\nEXP: {EXP.ToShortDateString()}";
            return dr;
        }
        /// <summary>
        /// Метод возвращает true если профиль заполнен полностью. В ином случае false.
        /// </summary>
        public bool IsFullInfo()
        {
            
            if(NameSurname == DEFAULT_NAME_SURNAME || Number == DEFAULT_NUMBER || Adress == DEFAULT_ADRESS || DOB == DEFAULT_DOB ||
                PhotoPath == DEFAULT_PHOTO_PATH || HGT == DEFAULT_HGT || ISS == DEFAULT_ISS || EXP == DEFAULT_EXP || Class_License == DEFAULT_CLASS_LICENSE) return false;
            else return true;
            // Данный метод используется для отслеживания заполенения профиля.
        }
    }
}
