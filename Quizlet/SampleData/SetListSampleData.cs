using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quizlet.Models;
using Quizlet.ViewModels;

namespace Quizlet.SampleData
{
    public class SetListSampleData
    {
        public ObservableCollection<Set> SetSampleData => new ObservableCollection<Set>()
        {
            new Set() {title = "мой первый сет", term_count = 25, created_by = "you"},
            new Set() {title = "мурмурмур", term_count = 172, created_by = "you"},
            new Set() {title = "виды кошечек", term_count = 64, created_by = "catFucker"},
            new Set() {title = "почкование, диплом какое длинное слово-то, пиздец вообще", term_count = 192, created_by = "you"},
            new Set() {title = "камасутра", term_count = 38, created_by = "you"},
        };

        public ObservableCollection<Term> ListOfTerms => new ObservableCollection<Term>()
        {
            new Term() { term = "бег", definition="run"},
            new Term() { term = "скакать", definition="ride"},
            new Term() { term = "огонь", definition="fire"},
            new Term() { term = "уведомлять", definition="notify"},
            new Term() { term = "становиться", definition="became"},
        };

        public ObservableCollection<TermCardViewModel> ContextTerms => new ObservableCollection<TermCardViewModel>()
        {
            new TermCardViewModel(){
            Term = new Term() { term = "бег", definition="run"} },
            new TermCardViewModel(){
            Term = new Term() { term = "скакать", definition="ride"} },
            new TermCardViewModel(){
            Term= new Term() { term = "огонь", definition="fire"} }
        };
    }
}
