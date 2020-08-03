using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLIV_Andreja_Kolesar.Model
{
    public class User
    {
        #region PROPERTY
        public string username { get; set; }
        public string password { get; set; }
        public string role
        {
            get
            {
                if (username.ToLower() == "zaposleni" && password.ToLower() == "zaposleni")
                {
                    return "zaposleni";
                }
                else if (password.ToLower() == "gost" && validJmbg(username))
                {
                    return "gost";
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region JMBG VALIDATION
        public bool validJmbg(string jmbg)
        {
            DateTime date = new DateTime();
            if (jmbg.Length != 13)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < 13; i++)
                {
                    if (!char.IsDigit(jmbg[i]))
                    {
                        return false;
                    }
                }

                DateTime now = DateTime.Now;
                char[] stringCurrentYear = now.Year.ToString().Substring(1).ToCharArray();
                string day = jmbg.Substring(0, 2);
                string month = jmbg.Substring(2, 2);
                string partOfyear = jmbg.Substring(4, 3);

                if (jmbg.Substring(4, 1) == "9")
                {
                    partOfyear = "1" + partOfyear;
                }
                else
                {
                    partOfyear = "2" + partOfyear;
                }
                string date_of_birth = partOfyear + "-" + month + "-" + day;
                try
                {
                    date = DateTime.Parse(date_of_birth);
                    if (date <= now)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }
        #endregion

    }
}
