using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DbStory.Provider
{
    public class SqlServerContentProvider : IContentProvider
    {
        private readonly string connectionString;

        public SqlServerContentProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private const string Query = @"
select 
	s.name as SchemaName, 
	sp.name as Name, 
	sc.text as Content 
from sys.procedures sp
inner join sys.syscomments sc on
	sc.id = sp.object_id
inner join sys.schemas s on
	s.schema_id = sp.schema_id
";

        private IDbConnection Connection
        {
            get { return new SqlConnection(connectionString); }
        }

        /// <summary>
        ///   Read all store procedures and functions from the sql server.
        /// </summary>
        /// <returns> </returns>
        public IEnumerable<ProgrammablityContent> GetFileContents()
        {
            IEnumerable<SqlContent> sqlContents;

            using (var connection = Connection)
            {
                connection.Open();
                sqlContents = connection.Query<SqlContent>(Query);
            }

            return sqlContents.Select(sqlContent => new ProgrammablityContent
                                                        {
                                                            Content = sqlContent.Content,
                                                            RelativeDirectoryName = "Store Procedures",
                                                            FileName = sqlContent.SchemaName + "." + sqlContent.Name + ".sql"
                                                        });
        }

        private class SqlContent
        {
            public string SchemaName { get; set; }
            public string Name { get; set; }
            public string Content { get; set; }
        }
    }
}