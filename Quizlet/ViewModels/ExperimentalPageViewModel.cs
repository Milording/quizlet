using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Quizlet.Models;
using Quizlet.Services;

namespace Quizlet.ViewModels
{
    public class Animal
    {
        public string Name { get; set; }
        public string Gender { get; set; }
    }

    public class ExperimentalPageViewModel:ViewModelBase
    {

        #region Constructor

        public ExperimentalPageViewModel()
        {
            var listAnimals = new ObservableCollection<Animal>()
            {
                new Animal()
                {
                    Gender = "Мужской",
                    Name = "Собак"
                },
                new Animal()
                {
                    Gender = "Мужской",
                    Name = "Ленивец"
                },
                new Animal()
                {
                    Gender = "Женский",
                    Name = "Курьма"
                },
                new Animal()
                {
                    Gender = "Женский",
                    Name = "Лысовица"
                },
                new Animal()
                {
                    Gender = "Мужской",
                    Name = "Муркун"
                }
            };

            var groups = new List<GroupInfoList>();
            var groupQuery =
                listAnimals.OrderBy(element => element.Name)
                    .GroupBy(element => element.Gender);

            //foreach (var g in groupQuery)
            //{
            //    GroupInfoList example = new GroupInfoList();
            //    example.Key = g.GroupName;
            //    foreach (var element in g.Items)
            //    {
            //        example.Add(example);
            //    }
            //    groups.Add(example);
            //}
            this.GroupInfoList = groupQuery;

        }

        #endregion

        #region Commands 
        

        #endregion
        
        #region Commands handlers

        private CollectionViewSource collection;

        public CollectionViewSource Collection
        {
            get
            {
                this.collection=new CollectionViewSource();
                var animals = new ObservableCollection<Animal>()
                {
                    new Animal()
                    {
                        Name = "Собака",
                        Gender = "Женский"
                    },
                    new Animal()
                    {
                        Name = "Лев",
                        Gender = "Мужской"
                    },
                    new Animal()
                    {
                        Name = "Пожарник",
                        Gender = "Мужской"
                    },
                    new Animal()
                    {
                        Name = "Мой код",
                        Gender = "Женский"
                    }
                };
                var groupedAnimals = animals.GroupBy(s => s.Gender);
                this.collection.Source = groupedAnimals;
                this.collection.IsSourceGrouped = true;
                return this.collection;
            }
        }

        ObservableCollection<Animal> animalList;

        ObservableCollection<Animal> AnimalList
        {
            get { return this.animalList; }
            set { SetProperty(ref this.animalList, value); }
        }

        #endregion

        #region Bindable properties

        private IEnumerable<IGrouping<string,Animal>> groupInfoList;

        public IEnumerable<IGrouping<string, Animal>> GroupInfoList
        {
            get
            {
                return this.groupInfoList;
            }
            set { SetProperty(ref this.groupInfoList, value); }
        }

        #endregion

        #region Private methods

        public async Task<List<GroupInfoList>> GroupData(List<Animal> listAnimals)
        {
            return await Task.Run(() =>
            {
                var groups = new List<GroupInfoList>();
                var groupQuery = from element in listAnimals
                    orderby element.Name
                    group element by element.Gender
                    into g
                    select new {GroupName = g.Key, Items = g};
                foreach (var g in groupQuery)
                {
                    GroupInfoList example = new GroupInfoList();
                    example.Key = g.GroupName;
                    foreach (var element in g.Items)
                    {
                        example.Add(example);
                    }
                }
                return groups;
            });
        }

        #endregion

    }
}
