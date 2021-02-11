using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RethinkDb.Driver;
using RethinkDb.Driver.Net;

namespace NHShareBack.Db
{
    class DbSystem
    {
        RethinkDB R1 { get; }

        private Connection _conn;
        private const string _dbName = "NHShare";

        private const string _userTableName = "Users";

        public DbSystem()
        {
            R1 = RethinkDB.R;
        }

        public async Task InitAsync()
            => await InitAsync(_dbName);

        public async Task InitAsync(string DbName)
        {
            _conn = await R1.Connection().ConnectAsync();
            if (!await R1.DbList().Contains(DbName).RunAsync<bool>(_conn)) {
                R1.DbCreate(DbName).Run(_conn);
            }
            if (!await R1.Db(DbName).TableList().Contains(_userTableName).RunAsync<bool>(_conn)) {
                R1.Db(DbName).TableCreate(_userTableName).Run(_conn);
            }
        }

        public async Task<bool> InitUserAsync(User user)
        {
            if (await R1.Db(_dbName).Table(_userTableName).GetAll(user.id).Count().Eq(0).RunAsync<bool>(_conn)) {
                await R1.Db(_dbName).Table(_userTableName).Insert(user).RunAsync(_conn);
                return true;
            }

            return false;
        }

        //TODO: Implement mutli-collection search
        public async Task<Collection> GetCollectionAsync(string Author, string CollectionName = null)
        {
            User usr = await R1.Db(_dbName).Table(_userTableName).Get(Author).RunAsync<User>(_conn);

            return usr == null ? null : usr.Collections.FirstOrDefault();
        }
    }
}
