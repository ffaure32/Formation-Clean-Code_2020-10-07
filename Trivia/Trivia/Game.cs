using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    /// <summary>
    /// The Game
    /// </summary>
    public class Game
    {
        private const int pointsToWin = 6;
        private const int numberOfQuestionsPerCategory = 50;
        private const int minNumberOfPlayers = 2;
        private const int numberOfPlaces = 12;
        private const int categoryFrequency = 4;
        private readonly List<Player> _players = new List<Player>();

        private readonly Questions questions = new Questions();


        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;

        public Game()
        {
            for (var i = 0; i < numberOfQuestionsPerCategory; i++)
            {
                questions.AddQuestion("Pop", "Pop Question " + i);
                questions.AddQuestion("Science", "Science Question " + i);
                questions.AddQuestion("Sports", "Sports Question " + i);
                questions.AddQuestion("Rock", "Rock Question " + i);
            }
        }

        public bool IsPlayable()
        {
            return (HowManyPlayers() >= minNumberOfPlayers);
        }

        public bool Add(string playerName)
        {
            _players.Add(new Player(playerName));

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


            if (_players[_currentPlayer].InPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    //User is getting out of penalty box
                    _isGettingOutOfPenaltyBox = true;
                    //Write that user is getting out
                    Console.WriteLine(_players[_currentPlayer] + " is getting out of the penalty box");
                    Move(roll);
                    LogPlayersInfo();
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
                Move(roll);
                LogPlayersInfo();
                AskQuestion();
            }
        }

        private void LogPlayersInfo()
        {
            Console.WriteLine(_players[_currentPlayer]
                              + "'s new location is "
                              + _players[_currentPlayer].Place);
            Console.WriteLine("The category is " + CurrentCategory());
        }

        private void Move(int roll)
        {
            // add roll to place
            var player = _players[_currentPlayer];
            MovePlayer(roll, player);
        }

        private void MovePlayer(int roll, Player player)
        {
            player.Place = player.Place + roll;
            if (player.Place >= numberOfPlaces)
            {
                player.Place = player.Place - numberOfPlaces;
            }
        }

        private void AskQuestion()
        {
            Console.WriteLine(questions.NextQuestion(CurrentCategory()));
        }

        private string CurrentCategory()
        {
            return (_players[_currentPlayer].Place % categoryFrequency) switch
            {
                0 => "Pop",
                1 => "Science",
                2 => "Sports",
                3 => "Rock",
                _ => null
            };
        }

        /// <summary>
        /// To call when the answer is right
        /// </summary>
        /// <returns></returns>
        public bool WasCorrectlyAnswered()
        {
            if (_players[_currentPlayer].InPenaltyBox)
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _players[_currentPlayer].Purse++;
                    Console.WriteLine(_players[_currentPlayer]
                            + " now has "
                            + _players[_currentPlayer].Purse
                            + " Gold Coins.");

                    var winner = !(_players[_currentPlayer].Purse == pointsToWin);
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
                _players[_currentPlayer].Purse++;
                Console.WriteLine(_players[_currentPlayer]
                        + " now has "
                        + _players[_currentPlayer].Purse
                        + " Gold Coins.");

                var winner = !(_players[_currentPlayer].Purse == pointsToWin);
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
            _players[_currentPlayer].InPenaltyBox = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) 
            {
                _currentPlayer = 0;
            }
            return true;
        }

        

    }

}
