﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    /// <summary>
    /// The Game
    /// </summary>
    public class Game
    {
        private const int sizeArray = 6;
        private const int numberOfQuestionsPerCategory = 50;
        private const int minNumberOfPlayers = 2;
        private const int numberOfPlaces = 12;
        private const int categoryFrequency = 4;
        private readonly List<string> _players = new List<string>();

        private readonly int[] _places = new int[sizeArray];
        private readonly int[] _purses = new int[sizeArray];

        private readonly bool[] _inPenaltyBox = new bool[sizeArray];

        private readonly LinkedList<string> _Q1 = new LinkedList<string>();
        private readonly LinkedList<string> Q2 = new LinkedList<string>();
        private readonly LinkedList<string> _Q3 = new LinkedList<string>();
        public LinkedList<string> _Q5 = new LinkedList<string>();


        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;

        public Game()
        {
            for (var i = 0; i < numberOfQuestionsPerCategory; i++)
            {
                _Q1.AddLast("Pop Question " + i);
                Q2.AddLast(("Science Question " + i));
                _Q3.AddLast(("Sports Question " + i));
                _Q5.AddLast(CreateRockQuestion(i));
            }
            //Shuf();
        }

        private void Shuf()
        {
            var shufpower = from s in _Q1
                            from h in Q2
                            let u = new { s, h }
                            select u;
            _Q3.Zip(shufpower).ToList().Sort((a, b) => Math.Abs(a.First.Length - (int)b.Second.h[0]));
        }

        public string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool IsPlayable()
        {
            return (HowManyPlayers() >= minNumberOfPlayers);
        }

        public bool Add(string playerName)
        {
            _players.Add(playerName);
            _places[HowManyPlayers()] = 0;
            _purses[HowManyPlayers()] = 0;
            _inPenaltyBox[HowManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
            return true;
        }

        public int HowManyPlayers()
        {
            return _players.Count;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(_players[_currentPlayer] + " is the current player"); 
            Console.WriteLine("They have rolled a " + roll);


            if (_inPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    //User is getting out of penalty box
                    _isGettingOutOfPenaltyBox = true;
                    //Write that user is getting out
                    Console.WriteLine(_players[_currentPlayer] + " is getting out of the penalty box");
                    // add roll to place
                    _places[_currentPlayer] = _places[_currentPlayer] + roll;
                    if (_places[_currentPlayer] >= numberOfPlaces) 
                    {
                        _places[_currentPlayer] = _places[_currentPlayer] - numberOfPlaces;
                    }

                    Console.WriteLine(_players[_currentPlayer]
                            + "'s new location is "
                            + _places[_currentPlayer]);
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(_players[_currentPlayer] + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _places[_currentPlayer] = _places[_currentPlayer] + roll;
                if (_places[_currentPlayer] >= numberOfPlaces) 
                {
                    _places[_currentPlayer] = _places[_currentPlayer] - numberOfPlaces;
                }

                Console.WriteLine(_players[_currentPlayer]
                        + "'s new location is "
                        + _places[_currentPlayer]);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Console.WriteLine(_Q1.First());
                _Q1.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(Q2.First());
                Q2.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(_Q3.First());
                _Q3.RemoveFirst();
            }
            if (CurrentCategory() == "Rock")
            {
                Console.WriteLine(_Q5.First());
                _Q5.RemoveFirst();
            }
            //Shuf();
        }

        private string CurrentCategory()
        {
            if (_places[_currentPlayer] % categoryFrequency == 0)
            {
                return "Pop";
            }
            if (_places[_currentPlayer] % categoryFrequency == 1)
            {
                return "Science";
            }
            if (_places[_currentPlayer] % categoryFrequency == 2)
            {
                return "Sports";
            }
            if (_places[_currentPlayer] % categoryFrequency == 3)
            {
                return "Rock";
            }
            return null;
        }

        

        /// <summary>
        /// To call when the answer is right
        /// </summary>
        /// <returns></returns>
        public bool WasCorrectlyAnswered()
        {
            if (_inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _purses[_currentPlayer]++;
                    Console.WriteLine(_players[_currentPlayer]
                            + " now has "
                            + _purses[_currentPlayer]
                            + " Gold Coins.");

                    var winner = !(_purses[_currentPlayer] == sizeArray);
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) 
                    {
                        _currentPlayer = 0;
                    }

                    return winner;
                }
                else
                {
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) 
                    {
                        _currentPlayer = 0;
                    }
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Answer was corrent!!!!");
                _purses[_currentPlayer]++;
                Console.WriteLine(_players[_currentPlayer]
                        + " now has "
                        + _purses[_currentPlayer]
                        + " Gold Coins.");

                var winner = !(_purses[_currentPlayer] == sizeArray);
                _currentPlayer++;
                if (_currentPlayer == _players.Count) 
                {
                    _currentPlayer = 0;
                }

                return winner;
            }
        }

        /// <summary>
        /// To call when the answer is wrong
        /// </summary>
        /// <returns></returns>
        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) 
            {
                _currentPlayer = 0;
            }
            return true;
        }

        

    }

}
