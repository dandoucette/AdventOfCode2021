using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    public class BingoCard
    {
        public int Id { get; set; }
        public BingoCard()
        {
            Numbers = new List<CardNumber>();
        }

        public List<CardNumber> Numbers { get; set; }

        private bool _isWinner;
        public bool IsWinner
        {
            get
            {
                if (_isWinner == false)
                {
                    if (Numbers.Where(x => x.IsCalled).Count() > 4)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (Numbers.Where(x => x.IsCalled && x.Column == i).Count() == 5 ||
                                Numbers.Where(x => x.IsCalled && x.Row == i).Count() == 5)
                            {
                                _isWinner = true;
                            }

                        }
                    }
                }
                return _isWinner;
            }
        }
    }
}
