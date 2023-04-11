using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace диплом
{
    public static class LevelCreator
    {
        static private int Number;
        static public List<Word> WordComponent = new List<Word>();
        static public CircleLetters CircleLetters;
        static List<string> Words = new List<string>();
        static List<Point> WordPoints = new List<Point>();
        static List<bool> Horizontal = new List<bool>();
        static string Letters;

        static public void DrawMap(Form f, int num)
        {
            Number = num;

            TakeParameters();

            for (int i = 0; i < Words.Count; i++)
            {
                Word w = new Word(Words[i], Horizontal[i], WordPoints[i]);
                WordComponent.Add(w);
            }

            for (int i = 0; i < Words.Count; i++)
            {
                WordComponent[i].DrawWord(f);
            }

            CircleLetters = new CircleLetters(f, new Point(700, 150), 300);
            CircleLetters.PlaceLabel(Letters);
        }

        static private void TakeParameters()
        {
            OleDbConnection myConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Db.accdb");
            myConnection.Open();

            OleDbCommand cmdWords = new OleDbCommand("SELECT Word FROM words WHERE Levels = ?", myConnection);
            cmdWords.Parameters.AddWithValue("@Level", Number);
            OleDbDataReader readerWords = cmdWords.ExecuteReader();

            OleDbCommand cmdHorizontal = new OleDbCommand("SELECT Horizontal FROM words WHERE Levels = ?", myConnection);
            cmdHorizontal.Parameters.AddWithValue("@Level", Number);
            OleDbDataReader readerHorizontal = cmdHorizontal.ExecuteReader();

            OleDbCommand cmdCoordinates = new OleDbCommand("SELECT X, Y FROM words WHERE Levels = ?", myConnection);
            cmdCoordinates.Parameters.AddWithValue("@Level", Number);
            OleDbDataReader readerCoordinates = cmdCoordinates.ExecuteReader();

            OleDbCommand cmdLetters = new OleDbCommand("SELECT letters FROM Levels WHERE id = " + Number.ToString(), myConnection);
            OleDbDataReader readerLetters = cmdLetters.ExecuteReader();

            while (readerWords.Read() && readerHorizontal.Read() && readerCoordinates.Read())
            {
                Words.Add(readerWords[0].ToString());
                Horizontal.Add((bool)readerHorizontal[0]);
                WordPoints.Add(new Point(Convert.ToInt32(readerCoordinates[0]), Convert.ToInt32(readerCoordinates[1])));
            }
            while (readerLetters.Read())
            {
                Letters = readerLetters[0].ToString();
            }

            readerWords.Close();
            readerHorizontal.Close();
            readerCoordinates.Close();
            readerLetters.Close();
        }
    }
}
