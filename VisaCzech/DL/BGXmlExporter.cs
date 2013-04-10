using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisaCzech.BL;
using VisaCzech.BL.BGModel;
using System.IO;
using System.Drawing.Imaging;

namespace VisaCzech.DL
{
    public static class BGXmlExporter
    {
        public static void Export(List<Person> persons, string savePath)
        {
            foreach (var person in persons)
            {
                var root = GetRoot(person);

                var fileName = Path.Combine(savePath, GenerateFileName(person));
                ModelIO.Save(root, fileName);
            }
        }

        private static string GenerateFileName(Person person)
        {
            return person.hdr_kscreated + person.hdr_regnom + ".xml";
        }

        private static RootLoadOsf GetRoot(Person person)
        {
            var root = new RootLoadOsf();
            root.msgHeader.msgHeaderRow.mh_datvav = DateTime.Now.ToString("yyyy-MM-dd");
            root.msgHeader.msgHeaderRow.mh_kscreated = person.hdr_kscreated;                // ***
            root.msgHeader.msgHeaderRow.mh_regnom = person.hdr_regnom;                      // ***
            root.msgHeader.msgHeaderRow.mh_usera = person.hdr_user;                         // *** 
            root.msgHeader.msgHeaderRow.mh_vfsrefno = person.Id;

            root.lcuz.lcuzRow.vid_zp = person.DocumentType == DocType.ForeignerPassport ? "C" : "P";
            root.lcuz.lcuzRow.nac_bel = ConvertCountry(person.DocumentIssuedBy);
            root.lcuz.lcuzRow.nac_pasp = person.DocumentNumber;
            root.lcuz.lcuzRow.pasp_val = ConvertDate(person.DocumentValidDate);
            root.lcuz.lcuzRow.graj = ConvertCountry(person.Citizenship);
            root.lcuz.lcuzRow.famil = person.Surname;
            root.lcuz.lcuzRow.imena = person.Name;
            root.lcuz.lcuzRow.dat_raj = ConvertDate(person.BirthDate, "yyyy/MM/dd");
            root.lcuz.lcuzRow.pol = person.Sex == Sex.Male ? "M" : "F";
            root.lcuz.lcuzRow.dat_izd = ConvertDate(person.DocumentIssuedDate);

            root.lcDop.lcDopRow.ld_mrjdarj = ConvertCountry(person.BirthCountry);
            root.lcDop.lcDopRow.ld_mrjnm = person.BirthPlace;
            root.lcDop.lcDopRow.ld_mrjgraj = ConvertCountry(person.BirthCitizenship);
            root.lcDop.lcDopRow.ld_zenen = ConvertStatus(person.Status);
            root.lcDop.lcDopRow.ld_jit_darj = ConvertCountry(person.AddressCountry);    // ***
            root.lcDop.lcDopRow.ld_jit_nm = person.AddressCity;                         // ***
            root.lcDop.lcDopRow.ld_jit_ul = person.AddressStreet;                       // ***
            root.lcDop.lcDopRow.ld_jit_pk = person.AddressZipCode;                      // ***
            root.lcDop.lcDopRow.ld_tel = person.PhoneNumber;
            root.lcDop.lcDopRow.ld_jit_email = person.Email;                            // ***
            root.lcDop.lcDopRow.ld_rabota = person.WorkName;                            // ***
            root.lcDop.lcDopRow.ld_profkod = ConvertProfCode(person.Profession);        // ***
            root.lcDop.lcDopRow.ld_profesia = person.Profession;
            root.lcDop.lcDopRow.ld_sl_darj = ConvertCountry(person.WorkCountry);        // ***
            root.lcDop.lcDopRow.ld_sl_nm = person.WorkCity;                             // ***
            root.lcDop.lcDopRow.ld_sl_ul = person.WorkAddress;                          // ***
            root.lcDop.lcDopRow.ld_sl_pk = person.WorkZip;                              // ***
            root.lcDop.lcDopRow.ld_sltel = person.WorkPhoneNumber;                      // ***
            root.lcDop.lcDopRow.ld_sl_fax = person.WorkFaxNumber;                       // ***
            root.lcDop.lcDopRow.ld_sl_email = person.WorkEmail;                         // ***

            root.basta.bastaRow.dc_famil = person.FatherSurname;                        // ***
            root.basta.bastaRow.dc_imena = person.FatherName;                           // ***

            root.maika.maikaRow.dc_famil = person.MotherSurname;                        // ***
            root.maika.maikaRow.dc_imena = person.MotherName;                           // ***

            if (person.Status == Status.Married)
            {
                root.sapruga.saprugaRow.sp_famil = person.SpouseSurname; // ***
                root.sapruga.saprugaRow.sp_famil2 = person.SpouseSurnameAtBirth; // ***
                root.sapruga.saprugaRow.sp_imena = person.SpouseName; // ***
                root.sapruga.saprugaRow.sp_datraj = ConvertDate(person.SpouseBirthDate, "yyyy/MM/dd"); // ***
                root.sapruga.saprugaRow.sp_mrjdarj = ConvertCountry(person.SpouseBirthCountry); // ***
                root.sapruga.saprugaRow.sp_mrjnm = person.SpouseBirthCity;
            }

            root.molba.molbaRow.dat_vli = ConvertDate(person.VisaStartDate);
            root.molba.molbaRow.dat_izl = ConvertDate(person.VisaEndDate);
            root.molba.molbaRow.vidvis = ConvertVisaType(person.VisaType);                                   // ***
            root.molba.molbaRow.brvl = ConvertEntries(person.NumberOfEntries);                              // ***
            root.molba.molbaRow.vidus = person.ProcessingSpeed ? "B" : "O";                             // ***
            root.molba.molbaRow.valvis = ConvertMultiVisaPeriod(person.MultiVisaPeriod);                            // ***
            root.molba.molbaRow.brdni = person.Duration;
            root.molba.molbaRow.cel = ConvertPurpose(person.Purpose);                       // ***
            root.molba.molbaRow.celdruga = person.OtherPurpose;                             // ***
            root.molba.molbaRow.mol_dat_vav = person.DateOfApply;  // ***
            root.molba.molbaRow.gratis = person.Gratis ? "Y" : "N";                         // ***
            root.molba.molbaRow.imavisa = person.TransitDestinationPermit ? "Y" : "N";                  // ***
            root.molba.molbaRow.cenamol = person.Fee;                                       // ***
            root.molba.molbaRow.cenacurr = person.FeeCurrency;                              // ***
            root.molba.molbaRow.maindest = person.DestinationCountry;                       // ***
            root.molba.molbaRow.maindestnm = person.DestinationCity;                        // ***
            root.molba.molbaRow.gkpp_darj = person.BorderOfFirstEntry;                      // ***
            root.molba.molbaRow.gkpp_text = person.BorderCheckpoint;                        // ***
            root.molba.molbaRow.marsrut = person.TransitRoute;                              // ***
            root.molba.molbaRow.Text_ini = person.VisaAdditionalInfo;                       // ***

            if (person.Purpose != Purpose.OrganizedTourism)
            {
                root.domakin.domakinRow.dm_vid = ConvertHosting(person.HostType); // ***
                root.domakin.domakinRow.nom_pok = person.InvitationNumber; // ***
                root.domakin.domakinRow.dom_graj = person.HostPersonCitizenship; // ***
                root.domakin.domakinRow.dom_famil = person.HostPersonSurname; // ***
                root.domakin.domakinRow.dom_ime = person.HostPersonName; // ***
                root.domakin.domakinRow.dom_egn = person.HostPersonID; // ***
                root.domakin.domakinRow.dom_darj = ConvertCountry(person.HostPersonCountry); // ***
                root.domakin.domakinRow.dom_nm = person.HostPersonCity; // ***
                root.domakin.domakinRow.dom_pk = person.HostPersonZipCode; // ***
                root.domakin.domakinRow.dom_adres = person.HostPersonAddress; // *** 
                root.domakin.domakinRow.dom_tel = person.HostPersonPhone; // ***
                root.domakin.domakinRow.dom_fax = person.HostPersonFax; // ***
                root.domakin.domakinRow.dom_email = person.HostPersonEmail; // ***
                root.domakin.domakinRow.ved_ekpou = person.HostCompanyID; // ***
                root.domakin.domakinRow.ved_ime = person.HotelName;
                root.domakin.domakinRow.ved_darj = ConvertCountry(person.HostCompanyCountry); // ***
                root.domakin.domakinRow.ved_nm = person.HostCompanyCity; // ***
                root.domakin.domakinRow.ved_pk = person.HostCompanyZipCode; // ***
                root.domakin.domakinRow.ved_adres = person.HostCompanyAddress; // ***
                root.domakin.domakinRow.ved_tel = person.HotelPhone;
                root.domakin.domakinRow.ved_fax = person.HotelFax; // ***
                root.domakin.domakinRow.ved_email = person.HostCompanyEmail; // ***
            }
            else
            {
                root.voit.voitRow.vnom = person.VoucherNumber;
                root.voit.voitRow.voit_datot = ConvertDate(person.VoucherValidFrom);
                root.voit.voitRow.voit_datdo = ConvertDate(person.VoucherValidTo);
                root.voit.voitRow.vime = person.VoucherIssuedBy;
                root.voit.voitRow.bgime = person.HostCompanyName;
                root.voit.voitRow.bgadres = (person.HostCompanyAddress + " " + person.HostCompanyCity).Trim();
                root.voit.voitRow.tel = person.HostCompanyPhone;
            }

            OldVisaRow oldVisa;
            if (person.Visa1Enabled)
            {
                oldVisa = new OldVisaRow
                              {
                                  ov_nacbel = person.Visa1Country,
                                  ov_vidvis = ConvertVisaType(person.Visa1Type),
                                  ov_visnom = person.Visa1Number,
                                  ov_dataot = ConvertDate(person.Visa1From),
                                  ov_datado = ConvertDate(person.Visa1To),
                                  ov_brvl = person.Visa1NumberOfEntries
                              };

                root.oldVisa.oldVisaRows.Add(oldVisa);
            }
            if (person.Visa2Enabled)
            {
                oldVisa = new OldVisaRow
                {
                    ov_nacbel = person.Visa2Country,
                    ov_vidvis = ConvertVisaType(person.Visa2Type),
                    ov_visnom = person.Visa2Number,
                    ov_dataot = ConvertDate(person.Visa2From),
                    ov_datado = ConvertDate(person.Visa2To),
                    ov_brvl = person.Visa2NumberOfEntries
                };

                root.oldVisa.oldVisaRows.Add(oldVisa);
            }
            if (person.Visa3Enabled)
            {
                oldVisa = new OldVisaRow
                {
                    ov_nacbel = person.Visa3Country,
                    ov_vidvis = ConvertVisaType(person.Visa3Type),
                    ov_visnom = person.Visa3Number,
                    ov_dataot = ConvertDate(person.Visa3From),
                    ov_datado = ConvertDate(person.Visa3To),
                    ov_brvl = person.Visa3NumberOfEntries
                };

                root.oldVisa.oldVisaRows.Add(oldVisa);
            }

            if (person.Image != null)
            {
                root.images.imagesRow.im_width = person.Image.Width.ToString();
                root.images.imagesRow.im_height = person.Image.Height.ToString();
                root.images.imagesRow.im_image = ImageConverter.ConvertImageToBase64(person.Image, ImageFormat.Jpeg);
                root.images.imagesRow.im_imglen = root.images.imagesRow.im_image.Length.ToString();
            }

            return root;
        }

        private static string ConvertMultiVisaPeriod(VisaPeriod visaPeriod)
        {
            throw new NotImplementedException();
        }

        private static string ConvertEntries(Entries entries)
        {
            throw new NotImplementedException();
        }

        private static string ConvertHosting(object p)
        {
            return string.Empty; throw new NotImplementedException();
        }

        private static string ConvertPurpose(object p)
        {
            return string.Empty; throw new NotImplementedException();
        }

        private static string ConvertVisaType(VisaType p)
        {
            return string.Empty; throw new NotImplementedException();
        }

        private static string ConvertProfCode(object p)
        {
            return string.Empty; throw new NotImplementedException();
        }

        private static string ConvertStatus(Status status)
        {
            return string.Empty; throw new NotImplementedException();
        }

        private static string ConvertDate(object date, string format = "yyyy-mm-dd")
        {
            if (date is string)
            {
                if ((date as string).Length == 8)
                    date = (date as string).Insert(4, ".").Insert(2, ".");
            }
            var realDate = Convert.ToDateTime(date);
            return realDate.ToString(format);
        }


        /// <summary>
        /// Преобразует трехбуквенный код страны в код для BG
        /// </summary>
        /// <param name="countryCode3"></param>
        /// <returns></returns>
        private static string ConvertCountry(string countryCode3)
        {
            return string.Empty; throw new NotImplementedException();
        }
    }
}
