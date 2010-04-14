using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FluentNHibernate;
using FubuCore;
using FubuMVC.StructureMap;
using Kokugen.Core;
using Kokugen.Core.Persistence;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using StructureMap;
using Configuration=NHibernate.Cfg.Configuration;

namespace Kokugen.Tests.SchemaCreation
{
    [TestFixture(Description = "DBSchema"), Explicit]
    public class DBSetupFixture
    {
        private ISessionSource _sessionSource;
        private IContainer container;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {

            container = StructureMapContainerFacility.GetBasicFubuContainer();

            container.Configure(x => x.AddRegistry(new SchemaRegistry()));

            //ObjectFactory.AssertConfigurationIsValid();
        }

        [SetUp]
        public void SetMeUp()
        {
            _sessionSource = container.GetInstance<ISessionSource>();
        }

        [Test, Category("DBSchema"), Explicit]
        public void CreateSchemaWithAllData()
        {
            Create_Database_Schema();
        }

        [Test, Explicit]
        public void Create_Database_Schema()
        {
            var sessSource = _sessionSource as NHibernatesessionSource;

            if (sessSource != null)
            {
                var conf = sessSource.AssembleConfiguration(null);

                //var exporter = new SchemaUpdate(conf);
                //exporter.SetOutputFile("ExecuteOutput.sql");
                //exporter.Create(false, true);

                //sessSource.BuildSchema();

                CreateOrUpdateSchema(conf);
            }
        }

        private static void CreateOrUpdateSchema(Configuration config)
        {
            string value = string.Empty;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["KokugenData"].ConnectionString))
            {
                con.Open();
                var cmd = con.CreateCommand();

                cmd.CommandText = @"IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' " +
                                    "AND TABLE_NAME='BoardColumns') "+
	                                "select 'True'; "+
                                    "ELSE "+
	                                 "select 'False' ";

                cmd.CommandType = CommandType.Text;

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    value = reader.GetString(0);
                }
            }


            // replace this with your test for existence of schema
            // (i.e., with SQLite, you can just test for the DB file)
            if (value.IsEmpty() || value == "False")
            {
                try
                {
                    var export = new SchemaExport(config);
                    export.Execute(true, true, false);
                }
                catch (HibernateException e)
                {
                    // create was not successful
                    // you problably want to break out your application here
                    Console.WriteLine(
                        String.Format("Problem while creating database: {0}", e),
                        "Problem");
                }
            }
            else
            {

                // already something: validate
                SchemaValidator validator = new SchemaValidator(config);
                try
                {
                    validator.Validate();
                }
                catch (HibernateException)
                {
                    // not valid, try to update
                    try
                    {
                        SchemaUpdate update = new SchemaUpdate(config);
                        update.Execute(true, true);

                        //var export = new SchemaExport(config);
                        //export.SetOutputFile("d:\\code\\coachesaid3\\schemaauto.txt");
                        //export.Execute(true, true, false);
                    }
                    catch (HibernateException e)
                    {
                        // update was not successful
                        // you problably want to break out your application here
                        Console.WriteLine(
                            String.Format("Problem while updating database: {0}", e),
                            "Problem");
                    }
                }
            }
        }
        
    }
}