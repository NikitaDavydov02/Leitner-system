using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leitner_System.Model;
using System.ComponentModel;

namespace Leitner_System.ViewModel
{
    class TrainingTemplateViewModel:INotifyPropertyChanged
    {
        public string TemplateName 
        {
            get { return trainingTemplate.TemplateName; }
            set { trainingTemplate.TemplateName = value; }
        }
        public string MaxCardCount
        {
            get { return trainingTemplate.maxCardsCount.ToString(); }
            set 
            {
                int newValue = -1;
                if (Int32.TryParse(value, out newValue))
                    trainingTemplate.maxCardsCount = newValue;

            }
        }
        
        private bool templateIsSelectedForTraining;
        public bool TemplateIsSelectedForTraining { get { return templateIsSelectedForTraining; }
            set
            {
                templateIsSelectedForTraining = value;
                OnPropertyChanged("TemplateIsSelectedForTraining");
            }
        }
        private TrainingTemplate trainingTemplate;
        public TrainingTemplateViewModel(TrainingTemplate trainingTemplate)
        {
            this.trainingTemplate = trainingTemplate;
            templateIsSelectedForTraining = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return TemplateName;
        }
    }
}
