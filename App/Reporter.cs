using System;
using System.Linq;
using System.Collections.Generic;
using CoreSchool.Entities;

namespace CoreSchool.App
{
    public class Reporter
    {
        Dictionary<DictionaryKey, IEnumerable<SchoolObjectBase>> _dictionary;
        public Reporter(Dictionary<DictionaryKey, IEnumerable<SchoolObjectBase>> dictionary)
        {
            if(dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }
            _dictionary = dictionary;
        }

        public IEnumerable<Evaluation> GetEvaluationsList()
        {
            if(_dictionary.TryGetValue(DictionaryKey.Evaluation, out IEnumerable<SchoolObjectBase> list))
            {
                return list.Cast<Evaluation>();
            }else
                return new List<Evaluation>();
        }

        public IEnumerable<string> GetSubjectsList()
        {
            return GetSubjectsList(out var dummy);
        }

        public IEnumerable<string> GetSubjectsList(out IEnumerable<Evaluation> evaluationsList)
        {
            evaluationsList = GetEvaluationsList();

            return (from ev in evaluationsList
                    select ev.Subject.Name).Distinct();
        }

        public Dictionary<string, IEnumerable<Evaluation>> GetEvaluationsXSubjectDictionary()
        {
            Dictionary<string, IEnumerable<Evaluation>> rtaDictionary = new Dictionary<string, IEnumerable<Evaluation>>();

            var SubjList = GetSubjectsList(out var EvalList);

            foreach (var subj in SubjList)
            {
                var subEva = from eval in EvalList
                             where eval.Subject.Name == subj
                             select eval;
                rtaDictionary.Add(subj, subEva);
            }

            return rtaDictionary;
        }

        public Dictionary<string, IEnumerable<AverageStudent>> GetAvgStudentXSubject()
        {
            var rta = new Dictionary<string, IEnumerable<AverageStudent>>();
            var evaXSubDict = GetEvaluationsXSubjectDictionary();

            foreach (var subjEva in evaXSubDict)
            {
                var averageStudent = from eval in subjEva.Value
                group eval by new { eval.Student.UniqueId, eval.Student.Name }
                into EvalStudentGroup
                            select new AverageStudent
                            {
                                studentId = EvalStudentGroup.Key.UniqueId,
                                studentName = EvalStudentGroup.Key.Name,
                                average = EvalStudentGroup.Average( eval => eval.Score)
                            };
                rta.Add(subjEva.Key, averageStudent);
            }
            return rta;
        }

        public Dictionary<string, IEnumerable<Object>> GetBestAverageXSubject(int quantity)
        {
            var rta = new Dictionary<string, IEnumerable<Object>>();

            var promStudent = GetAvgStudentXSubject();
            foreach (var item in promStudent)
            {
                var average = (from av in item.Value
                                orderby av.average descending
                                select new {
                                    Student = av.studentName,
                                    Average = av.average
                                }).Take(quantity);
                rta.Add(item.Key, average);
            }
            
            return rta;
        }
    }
}