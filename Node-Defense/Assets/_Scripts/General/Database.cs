using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class Database
{
    private string connectionPath;
    private IDbConnection dbconn;

    public Database()
    {
        connectionPath = "URI=file:" + Application.dataPath + "/Ranking.s3db"; //Path to database.
        dbconn = new SqliteConnection(connectionPath);

        //DropTableRankingRecords();
        CreateTableRankingRecords();
    }

    private void PostQueryToDb(string query)
    {
        try
        {
            dbconn.Open();

            IDbCommand command = dbconn.CreateCommand();
            command.CommandText = query;
            command.ExecuteReader();

            command.Dispose();
            command = null;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        finally
        {
            dbconn.Close();
        }
    }

    private void DropTableRankingRecords()
    {
        string query = "DROP TABLE IF EXISTS RankingRecords";
        this.PostQueryToDb(query);
    }

    private void CreateTableRankingRecords()
    {
        string query = 
            "CREATE TABLE IF NOT EXISTS RankingRecords ( " +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Name VARCHAR(200) NOT NULL, " +
                "Wave INTEGER NOT NULL, " +
                "Score INTEGER NOT NULL)";
        this.PostQueryToDb(query);
    }

    public void AddRankingRecord(RankingModel record)
    {
        string query = string.Format(
            "INSERT INTO RankingRecords " +
                "(Name, Wave, Score) " +
                "VALUES ('{0}',{1},{2})", 
                record.NameValue, 
                record.WaveValue, 
                record.ScoreValue);
        this.PostQueryToDb(query);
    }

    public List<RankingModel> GetAllRankingRecords()
    {
        var records = new List<RankingModel>();

        try
        {
            dbconn.Open();

            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT Id, Name, Wave, Score FROM RankingRecords WHERE Score > 0";
            dbcmd.CommandText = sqlQuery;

            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int wave = reader.GetInt32(2);
                int score = reader.GetInt32(3);

                var record = new RankingModel(name, wave, score);
                record.Id = id;
                records.Add(record);

                Debug.Log(string.Format("id: {0} \t name: {1} \t wave: {2} \t score: {3}", id, name, wave, score));
            }

            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        finally
        {
            dbconn.Close();
        }

        return records;
    }
}
