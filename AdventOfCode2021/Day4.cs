using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day4
    {
        public static int A()
        {
            var lines = File.ReadLines("InputData\\Day4.txt").ToList();
            var numbers = lines[0].Split(',');
            var cards = new List<BingoCard>();
            var lastFiveLines = new List<string>();

            foreach (var line in lines)
            {
                if (line.Length != 14)
                {
                    if(lastFiveLines.Count == 5)
                    {
                        var card = new BingoCard();
                        for (int y = 0; y < lastFiveLines.Count; y++)
                        {
                            var nums = lastFiveLines[y].Split(' ');
                            nums = nums.Where(x => x != string.Empty).ToArray();

                            for (int x = 0; x < nums.Length; x++)
                            {
                                card.Numbers.Add(new CardNumber
                                {
                                    Number = int.Parse(nums[x]),
                                    Column = x,
                                    Row = y,
                                    IsCalled = false
                                });
                            }
                        }
                        cards.Add(card);
                        lastFiveLines.Clear();
                    }
                    continue;
                }

                lastFiveLines.Add(line);
            }

            var lastCard = new BingoCard();
            for (int y = 0; y < lastFiveLines.Count; y++)
            {
                var nums = lastFiveLines[y].Split(' ');
                nums = nums.Where(x => x != string.Empty).ToArray();

                for (int x = 0; x < nums.Length; x++)
                {
                    lastCard.Numbers.Add(new CardNumber
                    {
                        Number = int.Parse(nums[x]),
                        Column = x,
                        Row = y,
                        IsCalled = false
                    });
                }
            }
            cards.Add(lastCard);

            BingoCard winningCard = null;
            int lastNumberCalled = 0;
            foreach (var number in numbers)
            {
                int calledNumber = int.Parse(number);
                lastNumberCalled = calledNumber;

                foreach (var card in cards)
                {
                    foreach (var num in card.Numbers)
                    {
                        if (num.Number == calledNumber)
                            num.IsCalled = true;
                    }

                    if(card.IsWinner)
                    {
                        winningCard = card;
                        break;
                    }
                }

                if (winningCard != null)
                    break;
            }
            int sumOfUnmarkedNumbers = winningCard.Numbers.Where(x => !x.IsCalled).Sum(x => x.Number);
            return sumOfUnmarkedNumbers * lastNumberCalled;
        }
    
        public static int B()
        {
            BingoCard CreateCard(List<string> fiveLines, int id)
            {
                var card = new BingoCard 
                { 
                    Id = id
                };

                for (int y = 0; y < fiveLines.Count; y++)
                {
                    var nums = fiveLines[y].Split(' ');
                    nums = nums.Where(x => x != string.Empty).ToArray();

                    for (int x = 0; x < nums.Length; x++)
                    {
                        card.Numbers.Add(new CardNumber
                        {
                            Number = int.Parse(nums[x]),
                            Column = x,
                            Row = y,
                            IsCalled = false
                        });
                    }
                }
                return card;
            }

            var lines = File.ReadLines("InputData\\Day4.txt").ToList();
            var numbers = lines[0].Split(',');
            var cards = new List<BingoCard>();
            var lastFiveLines = new List<string>();
            var cardCount = 1;

            foreach (var line in lines)
            {
                if (line.Length != 14)
                {
                    if (lastFiveLines.Count == 5)
                    {                    
                        cards.Add(CreateCard(lastFiveLines, cardCount));
                        lastFiveLines.Clear();
                        cardCount++;
                    }
                    continue;
                }

                lastFiveLines.Add(line);
            }

            cards.Add(CreateCard(lastFiveLines, cardCount));

            BingoCard lastCardToWin = null;
            int lastNumberCalled = 0;
            var numbersCalled = new List<int>();

            foreach (var number in numbers)
            {
                int calledNumber = int.Parse(number);
                numbersCalled.Add(calledNumber);
                lastNumberCalled = calledNumber;

                foreach (var card in cards)
                {
                    foreach (var num in card.Numbers)
                    {
                        if (num.Number == calledNumber)
                            num.IsCalled = true;
                    }
                }

                cards = cards.Where(x => x.IsWinner == false).ToList();
                
                if (cards.Count == 0)
                    break;

                if (cards.Count == 1)
                {
                    lastCardToWin = cards.First();
                }
            }
            int sumOfUnmarkedNumbers = lastCardToWin.Numbers.Where(x => !x.IsCalled).Sum(x => x.Number);
            return sumOfUnmarkedNumbers * lastNumberCalled;
        }
    }
}
