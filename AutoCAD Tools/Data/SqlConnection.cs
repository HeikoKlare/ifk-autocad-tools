using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using AutoCADTools.Management;

namespace AutoCADTools.Data
{
    class SqlConnection : IDisposable
    {
        #region Attributes

        private MySqlConnection connection;
        private MySqlDataAdapter dataAdapter;
        private bool disposed;

        #endregion

        #region Constructors

        public SqlConnection()
        {
            String connectionString =
                "server=" + Properties.Settings.Default.SqlConnectionPath + ";" +
                "user=" + Properties.Settings.Default.SqlConnectionLogin + ";" +
                "database=" + Properties.Settings.Default.SqlConnectionDatabase + ";" +
                "port=" + Properties.Settings.Default.SqlConnectionPort + ";" +
                "password=" + Properties.Settings.Default.SqlConnectionPassword + ";" +
                "connection timeout=" + Properties.Settings.Default.SqlConnectionTimeout;
            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (Exception)
            {
                MessageBox.Show(LocalData.NoConnectionText, LocalData.NoConnectionTitle, 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            dataAdapter = new MySqlDataAdapter();
        }

        #endregion

        #region Helper

        private void ReconnectWhenClosed()
        {
            if (connection.State != System.Data.ConnectionState.Open) {
                connection.Open();
            }
        }

        #endregion

        #region Employers

        public void FillEmployers(Database.EmployerDataTable employersTable)  
        {
            if (employersTable == null) return;

            ReconnectWhenClosed();

            dataAdapter.SelectCommand = new MySqlCommand(Properties.Settings.Default.SelectEmployersCommand, connection);
            using (MySqlCommandBuilder cb = new MySqlCommandBuilder(dataAdapter))
            {
                dataAdapter.Fill(employersTable);
            }
        }

        public void UpdateEmployers(Database.EmployerDataTable employersTable)
        {
            if (employersTable == null) return;

            ReconnectWhenClosed();

            dataAdapter.SelectCommand = new MySqlCommand(Properties.Settings.Default.SelectEmployersCommand, connection);
            using (MySqlCommandBuilder cb = new MySqlCommandBuilder(dataAdapter))
            {
                dataAdapter.Update(employersTable);
            }
        }

        #endregion

        #region Projects

        public void FillProjects(Database.ProjectDataTable projectsTable)
        {
            if (projectsTable == null) return;

            ReconnectWhenClosed();
            ReconnectWhenClosed();
            dataAdapter.SelectCommand = new MySqlCommand(Properties.Settings.Default.SelectProjectsCommand, connection);
            using (MySqlCommandBuilder cb = new MySqlCommandBuilder(dataAdapter))
            {
                dataAdapter.Fill(projectsTable);
            }
        }

        public void UpdateProjects(Database.ProjectDataTable projectsTable)
        {
            if (projectsTable == null) return;

            ReconnectWhenClosed();

            dataAdapter.SelectCommand = new MySqlCommand(Properties.Settings.Default.SelectProjectsCommand, connection);
            using (MySqlCommandBuilder cb = new MySqlCommandBuilder(dataAdapter))
            {
                dataAdapter.Update(projectsTable);
            }
        }

        #endregion

        #region Annotations

        public void FillAnnotations(Database.AnnotationsDataTable annotationsTable, int id)
        {
            if (annotationsTable == null) return;

            ReconnectWhenClosed();

            dataAdapter.SelectCommand = new MySqlCommand(Properties.Settings.Default.SelectAnnotationsCommand + " WHERE categoryId = " + id + " ORDER BY name", connection);
            using (MySqlCommandBuilder cb = new MySqlCommandBuilder(dataAdapter))
            {
                dataAdapter.Fill(annotationsTable);
            }
        }

        public void UpdateAnnotations(Database.AnnotationsDataTable annotationsTable)
        {
            if (annotationsTable == null) return;

            ReconnectWhenClosed();

            dataAdapter.SelectCommand = new MySqlCommand(Properties.Settings.Default.SelectAnnotationsCommand, connection);
            using (MySqlCommandBuilder cb = new MySqlCommandBuilder(dataAdapter))
            {
                dataAdapter.Update(annotationsTable);
            }
        }

        #endregion

        #region AnnotationCategories

        public void FillAnnotationCategories(Database.AnnotationCategoriesDataTable annotationCategoriesTable)
        {
            if (annotationCategoriesTable == null) return;

            ReconnectWhenClosed();

            dataAdapter.SelectCommand = new MySqlCommand(Properties.Settings.Default.SelectAnnotationCategoriesCommand, connection);
            using (MySqlCommandBuilder cb = new MySqlCommandBuilder(dataAdapter))
            {
                dataAdapter.Fill(annotationCategoriesTable);
            }
        }

        public void UpdateAnnotationCategories(Database.AnnotationCategoriesDataTable annotationCategoriesTable)
        {
            if (annotationCategoriesTable == null) return;

            ReconnectWhenClosed();

            dataAdapter.SelectCommand = new MySqlCommand(Properties.Settings.Default.SelectAnnotationCategoriesCommand, connection);
            using (MySqlCommandBuilder cb = new MySqlCommandBuilder(dataAdapter))
            {
                dataAdapter.Update(annotationCategoriesTable);
            }
        }
        
        #endregion

        #region Details

        public void FillDetails(Database.DetailsDataTable detailsTable, int id)
        {
            if (detailsTable == null) return;

            ReconnectWhenClosed();

            dataAdapter.SelectCommand = new MySqlCommand(Properties.Settings.Default.SelectDetailsReducedCommand + " WHERE categoryId = " + id + " ORDER BY name", connection);
            using (MySqlCommandBuilder cb = new MySqlCommandBuilder(dataAdapter))
            {
                //System.Windows.MessageBox.Show(dataAdapter.UpdateCommand.CommandText);
                dataAdapter.Fill(detailsTable);
            }
        }

        public void UpdateDetails(Database.DetailsDataTable detailsTable)
        {
            if (detailsTable == null) return;

            ReconnectWhenClosed();

            dataAdapter.SelectCommand = new MySqlCommand(Properties.Settings.Default.SelectDetailsReducedCommand, connection);
            using (MySqlCommandBuilder cb = new MySqlCommandBuilder(dataAdapter))
            {
                dataAdapter.Update(detailsTable);
            }
        }

        public Database.DetailsDataTable GetDetail(int id)
        {
            ReconnectWhenClosed();

            dataAdapter.SelectCommand = new MySqlCommand(Properties.Settings.Default.SelectSpecificDetailCommand, connection);
            dataAdapter.SelectCommand.Parameters.AddWithValue("?id", id);
            Database.DetailsDataTable table = new Database.DetailsDataTable();
            dataAdapter.Fill(table);

            return table;
        }

        public void FillDetailsComplete(Database.DetailsDataTable detailsTable, int id)
        {
            if (detailsTable == null) return;

            ReconnectWhenClosed();

            dataAdapter.SelectCommand = new MySqlCommand(Properties.Settings.Default.SelectDetailsCommand + " WHERE categoryId = " + id + " ORDER BY name", connection);
            using (MySqlCommandBuilder cb = new MySqlCommandBuilder(dataAdapter))
            {
                //System.Windows.MessageBox.Show(dataAdapter.UpdateCommand.CommandText);
                dataAdapter.Fill(detailsTable);
            }
        }

        public void UpdateDetailsComplete(Database.DetailsDataTable detailsTable)
        {
            if (detailsTable == null) return;

            ReconnectWhenClosed();

            dataAdapter.SelectCommand = new MySqlCommand(Properties.Settings.Default.SelectDetailsCommand, connection);
            using (MySqlCommandBuilder cb = new MySqlCommandBuilder(dataAdapter))
            {
                dataAdapter.Update(detailsTable);
            }
        }

        #endregion

        #region DetailCategories

        public void FillDetailCategories(Database.DetailCategoriesDataTable detailCategoriesTable)
        {
            if (detailCategoriesTable == null) return;

            ReconnectWhenClosed();

            dataAdapter.SelectCommand = new MySqlCommand(Properties.Settings.Default.SelectDetailCategoriesCommand, connection);
            using (MySqlCommandBuilder cb = new MySqlCommandBuilder(dataAdapter))
            {
                dataAdapter.Fill(detailCategoriesTable);
            }
        }

        public void UpdateDetailCategories(Database.DetailCategoriesDataTable detailCategoriesTable)
        {
            if (detailCategoriesTable == null) return;

            ReconnectWhenClosed();

            dataAdapter.SelectCommand = new MySqlCommand(Properties.Settings.Default.SelectDetailCategoriesCommand, connection);
            using (MySqlCommandBuilder cb = new MySqlCommandBuilder(dataAdapter))
            {
                dataAdapter.Update(detailCategoriesTable);
            }
        }

        #endregion

        #region Disposing

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    connection.Close();
                    connection = null;
                    dataAdapter.Dispose();
                    dataAdapter = null;
                }
                disposed = true;
            }
        }

        #endregion

    }
}
