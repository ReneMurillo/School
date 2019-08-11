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

        public IEnumerable<Evaluations> GetEvaluationsList()
        {
            if(_dictionary.TryGetValue(DictionaryKey.Evaluation, out IEnumerable<SchoolObjectBase> list))
            {
                return list.Cast<Evaluations>();
            }else
                return new List<Evaluations>();
        }

        public IEnumerable<string> GetSubjectsList()
        {
            return GetSubjectsList(out var dummy);
        }

        public IEnumerable<string> GetSubjectsList(out IEnumerable<Evaluations> evaluationsList)
        {
            evaluationsList = GetEvaluationsList();

            return (from ev in evaluationsList
                    select ev.Subject.Name).Distinct();
        }

        public Dictionary<string, IEnumerable<Evaluations>> GetEvaluationsXSubjectDictionary()
        {
            Dictionary<string, IEnumerable<Evaluations>> rtaDictionary = new Dictionary<string, IEnumerable<Evaluations>>();

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

        public Dictionary<string, IEnumerable<Object>> GetAvgStudentXSubject()
        {
            var rta = new Dictionary<string, IEnumerable<Object>>();
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
    }
}