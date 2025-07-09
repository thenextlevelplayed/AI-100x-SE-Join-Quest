using ChineseChess.Core.Models;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace ChineseChess.Tests.StepDefinitions
{
    [Binding]
    public class ChineseChessStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public ChineseChessStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _scenarioContext.Set(new Game());
        }

        private Game GetGame() => _scenarioContext.Get<Game>();

        [Given(@"the board is empty except for a (.*) (.*) at \((\d+), (\d+)\)")]
        public void GivenTheBoardIsEmptyExceptForA(string color, string type, int row, int col)
        {
            var game = GetGame();
            var pieceColor = Enum.Parse<PieceColor>(color, true);
            var pieceType = Enum.Parse<PieceType>(type, true);
            game.Board.AddPiece(new Piece(pieceType, pieceColor, new Position(row, col)));
        }

        [Given(@"the board has:")]
        public void GivenTheBoardHas(Table table)
        {
            var game = GetGame();
            foreach (var row in table.Rows)
            {
                var pieceFullName = row["Piece"];
                var positionRaw = row["Position"];

                var parts = pieceFullName.Split(' ');
                var color = Enum.Parse<PieceColor>(parts[0], true);
                var type = Enum.Parse<PieceType>(parts[1], true);

                var match = Regex.Match(positionRaw, @"\((\d+), (\d+)\)");
                var pieceRow = int.Parse(match.Groups[1].Value);
                var pieceCol = int.Parse(match.Groups[2].Value);

                game.Board.AddPiece(new Piece(type, color, new Position(pieceRow, pieceCol)));
            }
        }

        [When(@"(.*) moves the (.*) from \((\d+), (\d+)\) to \((\d+), (\d+)\)")]
        public void WhenPlayerMovesPiece(string color, string type, int fromRow, int fromCol, int toRow, int toCol)
        {
            var game = GetGame();
            var pieceColor = Enum.Parse<PieceColor>(color, true);
            
            // Ensure it's the correct player's turn
            if(game.CurrentPlayer != pieceColor)
            {
                game.SwitchPlayer();
            }

            var from = new Position(fromRow, fromCol);
            var to = new Position(toRow, toCol);

            var moveResult = game.MakeMove(from, to);
            _scenarioContext.Set(moveResult, "LastMoveResult");
        }

        [Then(@"the move is (.*)")]
        public void ThenTheMoveIs(string expectedResult)
        {
            var lastMoveResult = _scenarioContext.Get<bool>("LastMoveResult");
            bool expected = expectedResult.ToLower() == "legal";
            Assert.AreEqual(expected, lastMoveResult);
        }

        [Then(@"(.*) wins immediately")]
        public void ThenPlayerWinsImmediately(string color)
        {
            var game = GetGame();
            Assert.IsTrue(game.IsOver);
        }

        [Then(@"the game is not over just from that capture")]
        public void ThenTheGameIsNotOver()
        {
            var game = GetGame();
            Assert.IsFalse(game.IsOver);
        }
    }
} 