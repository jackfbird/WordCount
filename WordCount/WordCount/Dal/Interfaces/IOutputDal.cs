using WordCount.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordCount.Dal.Interfaces
{
    public interface IOutputDal
    {

        IDictionary<string, int> GetCountByWord();

        Output GetWordsAndSentences();


    }
}
