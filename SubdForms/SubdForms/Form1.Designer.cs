namespace SubdForms {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CreateBook = new System.Windows.Forms.Button();
            this.CreateGenre = new System.Windows.Forms.Button();
            this.ViewAuthors = new System.Windows.Forms.Button();
            this.ViewBooks = new System.Windows.Forms.Button();
            this.ViewGenres = new System.Windows.Forms.Button();
            this.CreateAuthor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreateBook
            // 
            this.CreateBook.Location = new System.Drawing.Point(12, 85);
            this.CreateBook.Name = "CreateBook";
            this.CreateBook.Size = new System.Drawing.Size(298, 67);
            this.CreateBook.TabIndex = 1;
            this.CreateBook.Text = "Create book";
            this.CreateBook.UseVisualStyleBackColor = true;
            this.CreateBook.Click += new System.EventHandler(this.CreateBook_Click);
            // 
            // CreateGenre
            // 
            this.CreateGenre.Location = new System.Drawing.Point(12, 158);
            this.CreateGenre.Name = "CreateGenre";
            this.CreateGenre.Size = new System.Drawing.Size(298, 67);
            this.CreateGenre.TabIndex = 3;
            this.CreateGenre.Text = "Create genre";
            this.CreateGenre.UseVisualStyleBackColor = true;
            this.CreateGenre.Click += new System.EventHandler(this.CreateGenre_Click);
            // 
            // ViewAuthors
            // 
            this.ViewAuthors.Location = new System.Drawing.Point(490, 12);
            this.ViewAuthors.Name = "ViewAuthors";
            this.ViewAuthors.Size = new System.Drawing.Size(298, 67);
            this.ViewAuthors.TabIndex = 4;
            this.ViewAuthors.Text = "View authors";
            this.ViewAuthors.UseVisualStyleBackColor = true;
            this.ViewAuthors.Click += new System.EventHandler(this.ViewAuthors_Click);
            // 
            // ViewBooks
            // 
            this.ViewBooks.Location = new System.Drawing.Point(490, 85);
            this.ViewBooks.Name = "ViewBooks";
            this.ViewBooks.Size = new System.Drawing.Size(298, 67);
            this.ViewBooks.TabIndex = 5;
            this.ViewBooks.Text = "View books";
            this.ViewBooks.UseVisualStyleBackColor = true;
            this.ViewBooks.Click += new System.EventHandler(this.ViewBooks_Click);
            // 
            // ViewGenres
            // 
            this.ViewGenres.Location = new System.Drawing.Point(490, 158);
            this.ViewGenres.Name = "ViewGenres";
            this.ViewGenres.Size = new System.Drawing.Size(298, 67);
            this.ViewGenres.TabIndex = 6;
            this.ViewGenres.Text = "View genres";
            this.ViewGenres.UseVisualStyleBackColor = true;
            this.ViewGenres.Click += new System.EventHandler(this.ViewGenres_Click);
            // 
            // CreateAuthor
            // 
            this.CreateAuthor.Location = new System.Drawing.Point(12, 12);
            this.CreateAuthor.Name = "CreateAuthor";
            this.CreateAuthor.Size = new System.Drawing.Size(298, 67);
            this.CreateAuthor.TabIndex = 7;
            this.CreateAuthor.Text = "Create author";
            this.CreateAuthor.UseVisualStyleBackColor = true;
            this.CreateAuthor.Click += new System.EventHandler(this.CreateAuthor_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CreateAuthor);
            this.Controls.Add(this.ViewGenres);
            this.Controls.Add(this.ViewBooks);
            this.Controls.Add(this.ViewAuthors);
            this.Controls.Add(this.CreateGenre);
            this.Controls.Add(this.CreateBook);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button CreateBook;
        private System.Windows.Forms.Button CreateGenre;
        private System.Windows.Forms.Button ViewAuthors;
        private System.Windows.Forms.Button ViewBooks;
        private System.Windows.Forms.Button ViewGenres;
        private System.Windows.Forms.Button CreateAuthor;
    }
}

