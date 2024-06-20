using DataGridFinalDemoAPI.Business_Logic.Interfaces;
using DataGridFinalDemoAPI.Models;

namespace DataGridFinalDemoAPI.Business_Logic.Handlers
{
    public class BLEDC01Handler : IEDC01Service
    {
        private readonly ISTU01Service _stu01Service;
        private readonly List<EDC01> _lstEDC01;
        private int _educationId;

        public BLEDC01Handler(ISTU01Service stu01Service)
        {
            _stu01Service = stu01Service;
            _educationId = 1;
            _lstEDC01 = GenerateEducationForStudents();
        }

        public List<EDC01> GetAll(int id)
        {
            return _lstEDC01.FindAll(edu => edu.C01F05 == id);
        }

        private List<EDC01> GenerateEducationForStudents()
        {
            Random random = new();
            List<string> schoolNames = new() { "School A", "School B", "School C", "School D", "School E" };
            List<string> standards = new () { "10th", "12th", "Bachelors", "Masters", "PhD" };
            List<EDC01> lstEDC01 = new();

            foreach (STU01 student in _stu01Service.GetAll())
            {
                for (int j = 0; j < 3; j++)
                {
                    string schoolName = schoolNames[random.Next(schoolNames.Count)];
                    string standard = standards[random.Next(standards.Count)];
                    double percentage = random.NextDouble() * (100 - 50) + 50; // Random percentage between 50 and 100

                    lstEDC01.Add(new EDC01
                    {
                        C01F01 = _educationId++,
                        C01F02 = schoolName,
                        C01F03 = standard,
                        C01F04 = Math.Round(percentage, 2),
                        C01F05 = student.U01F01
                    });
                }
            }

            return lstEDC01;
        }
    }
}
