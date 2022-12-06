﻿using MindWord.DataAccess.Interfaces.Repositories;
using MindWord.DataAccess.Repositories;
using MindWord.Domain.Entities;

namespace MindWord.Service.Services
{
    public class GameService
    {
        public async Task<List<List<string>>> RandomTestAsync()
        {
            Random random = new Random();
            List<List<string>> test = new List<List<string>>();
            IWordRepository repository = new WordRepository();
            var wordsDB = (await repository.GetAllAsync()).ToList();

            var words = Shuffle(wordsDB);

            for(int i=0; i < words.Count; i++)
            {
                List<string> list = new List<string>() { "","","","","",""};
                list[0] =words[i].Name;
                list[random.Next(1, 4)] = words[i].Translate;
                list[5] = words[i].Translate;
                while (list[1] == "" || list[2] == "" || list[3] == "" || list[4] == "")
                {
                    var res =  words[random.Next(0, words.Count)].Translate;
                    for (int l = 1; l < 5; l++)
                    {
                        if (list[l] == "" && !list.Contains(res))
                            list[l] = res;
                    }
                }
                test.Add(list);
            }
            return test;
        }
        public async Task<List<List<string>>> RandomTranslateTestAsync()
        {

            {
                Random random = new Random();
                List<List<string>> test = new List<List<string>>();
                IWordRepository repository = new WordRepository();
                var wordsDB = (await repository.GetAllAsync()).ToList();

                var words = Shuffle(wordsDB);

                for (int i = 0; i < words.Count; i++)
                {
                    List<string> list = new List<string>() { "", "", "", "", "", "" };
                    list[0] = words[i].Translate;
                    list[random.Next(1, 4)] = words[i].Name;
                    list[5] = words[i].Name;
                    while (list[1] == "" || list[2] == "" || list[3] == "" || list[4] == "")
                    {
                        var res = words[random.Next(0, words.Count)].Name;
                        for (int l = 1; l < 5; l++)
                        {
                            if (list[l] == "" && !list.Contains(res))
                                list[l] = res;
                        }
                    }
                    test.Add(list);
                }
                return test;
            }
        }
        public static List<Word> Shuffle(List<Word> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Word value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }
    }
}
