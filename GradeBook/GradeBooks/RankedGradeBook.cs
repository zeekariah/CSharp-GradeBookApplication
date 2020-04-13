using System;
using System.Collections.Generic;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if ( Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            double percent = Students.Count / 100 * 2;
            int twentyPercent = (int)Math.Round(percent);
            var gradeTotals = new List<double>();

            //get total for all grades
            foreach (Student student in Students)
            {
                gradeTotals.Add(student.AverageGrade);
            }

            gradeTotals.Sort();

            if (averageGrade >= gradeTotals[twentyPercent])
            {
                return 'A';
            }
            else if (averageGrade >= gradeTotals[twentyPercent * 2])
            {
                return 'B';
            }
            else if (averageGrade >= gradeTotals[twentyPercent * 3])
            {
                return 'C';
            }
            else if (averageGrade >= gradeTotals[twentyPercent * 4])
            {
                return 'C';
            }

            return 'F';
        }
    }
}