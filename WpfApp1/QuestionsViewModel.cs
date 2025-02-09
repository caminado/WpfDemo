using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Xml.Linq;

namespace WpfApp1.VM
{
    public class QuestionsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Section> _sections;

        public ObservableCollection<Section> Sections
        {
            get { return _sections; }
            set
            {
                _sections = value;
                OnPropertyChanged();
            }
        }


        public QuestionsViewModel()
        {
            LoadFileSystem();
        }

        private void LoadFileSystem()
        {
            var xml = XElement.Parse(@"
<QDM>
    <Sections>
        <Section id='1'>
            <Name>First Section</Name>
            <Questions>
                <Question id='1'/>
                <Question id='2'/>
            </Questions>
        </Section>
        <Section id='2'>
            <Name>Second Section</Name>
            <Questions>
                <Question id='1'/>
                <Question id='2'/>
            </Questions>
        </Section>
    </Sections>
    <Questions>
        <Question id='1'>
            <Text>Вы на собеседовании ?</Text>
            <AvAns>
                <Answer id='1'/>
                <Answer id='2'/>
            </AvAns>
        </Question>
        <Question id='2'>
            <Text>Ваше имя</Text>
            <AvAns>
                <Answer id='3'/>
                <Answer id='4'/>
            </AvAns>
        </Question>
    </Questions>
    <Answers>
        <Answer id='1'>Да</Answer>
        <Answer id='2'>Нет</Answer>
        <Answer id='3'>Олег</Answer>
        <Answer id='4'>Вадим</Answer>
    </Answers>
</QDM>");

            var sections = xml.Element("Sections").Elements("Section");
            var questions = xml.Element("Questions").Elements("Question");
            var answers = xml.Element("Answers").Elements("Answer");

            var sectionList = new List<Section>();
            foreach (var section in sections)
            {
                var sec = new Section
                {
                    Id = section.Attribute("id").Value,
                    Name2 = section.Element("Name").Value,
                    Questions = new List<Question>()
                };

                foreach (var questionRef in section.Element("Questions").Elements("Question"))
                {
                    var questionId = questionRef.Attribute("id").Value;
                    var question = questions.FirstOrDefault(q => q.Attribute("id").Value == questionId);
                    if (question != null)
                    {
                        var q = new Question
                        {
                            Id = questionId,
                            Text2 = question.Element("Text").Value,
                            AvAns = new List<string>()
                        };

                        foreach (var answerRef in question.Element("AvAns").Elements("Answer"))
                        {
                            var answerId = answerRef.Attribute("id").Value;
                            var answer = answers.FirstOrDefault(a => a.Attribute("id").Value == answerId);
                            if (answer != null)
                            {
                                q.AvAns.Add(answer.Value);
                            }
                        }

                        sec.Questions.Add(q);
                    }
                }

                sectionList.Add(sec);
                Sections = new ObservableCollection<Section>(sectionList);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand _addSectionCommand;
        public ICommand AddSectionCommand
        {
            get
            {
                if (_addSectionCommand == null)
                {
                    _addSectionCommand = new RelayCommand(AddSection);
                }
                return _addSectionCommand;
            }
        }
        private void AddSection( )
        {
            
        }
    }

    public class Section
    {
        public string Id { get; set; }
        public string Name2 { get; set; }
        public List<Question> Questions { get; set; }

        public int QuestionsCounter => Questions.Count;
    }

    public class Question
    {
        public string Id { get; set; }
        public string Text2 { get; set; }
        public List<string> AvAns { get; set; }
    }
}
