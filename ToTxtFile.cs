using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LearningDiary
{
    public class ToTxtFile
    {
        public static void TopicsToTxtfile(List<Topic> createdTopics, string url)
        {
            for (int i = 0; i < createdTopics.Count; i++)
            {
                foreach (var value in createdTopics[i].GetType().GetProperties())
                {
                    if (value.Name == "TaskList" && createdTopics[i].TaskList?.Any() == true)
                    {
                        TasksToTxtfile(createdTopics[i], url);
                        File.AppendAllText(url, "##");
                    }
                    else
                        File.AppendAllText(url, string.Format($"{value.Name.ToUpper()}: {value.GetValue(createdTopics[i])}##"));
                }

                File.AppendAllText(url, "#");
            }
        }

        public static void TasksToTxtfile(Topic oneTopic, string url)
        {
            File.AppendAllText(url, "TaskList: ");
            List<Task> listOfTasks = oneTopic.TaskList;
            for (int j = 0; j < listOfTasks.Count; j++)
            {
                File.AppendAllText(url, "*TASK*");
                foreach (var item in listOfTasks[j].GetType().GetProperties())
                {
                    if (item.Name == "Notes" && listOfTasks[j].Notes?.Any() == true)
                    {
                        File.AppendAllText(url, string.Format($"{item.Name.ToUpper()}: "));
                        NotesToTxtfile(listOfTasks[j], url);
                    }
                    else
                        File.AppendAllText(url, string.Format($"{item.Name.ToUpper()}: {item.GetValue(listOfTasks[j])}+++"));
                }
            }
        }

        public static void NotesToTxtfile(Task oneTask, string url)
        {
            for (int i = 0; i < oneTask.Notes.Count; i++)
            {
                File.AppendAllText(url, string.Format($"{oneTask.Notes[i]} "));
            }
            File.AppendAllText(url, "+++");
        }
    }
}