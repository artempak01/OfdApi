namespace OfdRuApi
{
    public class Company
    {
        public Company(string companyINN, string companyOGRN, string companyForm, string companyName, string companyKpp)
        {
            CompanyINN = companyINN;
            CompanyOGRN = companyOGRN;
            CompanyForm = companyForm;
            CompanyName = companyName;
            CompanyKPP = companyKpp;
        }

        public string CompanyINN { get; set; }
        public string CompanyKPP { get; set; }
        public string CompanyOGRN { get; set; }
        public string CompanyForm { get; set; }
        public string CompanyName { get; set; }
    }
}
