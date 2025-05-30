﻿using System;
using System.IO;
using System.Windows.Forms;

namespace Lab_6
{
    public class DocumentManager
    {
        public string OpenedDocumentPath { get; private set; } = string.Empty;
        public bool IsOpened { get; private set; } = false;
        public bool IsUnsaved { get; private set; } = false;
        public string DefaultSaveDirectory { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private readonly RichTextBox _editor;

        public DocumentManager(RichTextBox editor)
        {
            _editor = editor;
        }

        public void CreateNewDocument()
        {
            _editor.Clear();
            OpenedDocumentPath = string.Empty;
            IsOpened = false;
            IsUnsaved = false;
        }

        public void OpenDocument()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Markdown Files (*.md)|*.md|Rich Text Format (*.rtf)|*.rtf|Text Files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.Title = "Open File";
                openFileDialog.InitialDirectory = DefaultSaveDirectory;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string ext = Path.GetExtension(openFileDialog.FileName).ToLower();
                    if (ext == ".txt" || ext == ".md")
                        _editor.Text = File.ReadAllText(openFileDialog.FileName);
                    else
                        _editor.LoadFile(openFileDialog.FileName);

                    OpenedDocumentPath = openFileDialog.FileName;
                    IsOpened = true;
                    IsUnsaved = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening file: {ex.Message}");
                }
            }
            };
        }

        public void SaveDocument()
        {
            if (IsOpened && !string.IsNullOrEmpty(OpenedDocumentPath))
            {
                try
                {
                    string ext = Path.GetExtension(OpenedDocumentPath).ToLower();
                    if (ext == ".txt" || ext == ".md")
                        File.WriteAllText(OpenedDocumentPath, _editor.Text);
                    else
                        _editor.SaveFile(OpenedDocumentPath);
                    IsUnsaved = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving file: {ex.Message}");
                }
            }
            else
            {
                SaveDocumentAs();
            }
        }

        public void SaveDocumentAs()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Markdown Files (*.md)|*.md|Rich Text Format (*.rtf)|*.rtf|Text Files (*.txt)|*.txt";
                saveFileDialog.Title = "Save As";
                saveFileDialog.InitialDirectory = DefaultSaveDirectory;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(saveFileDialog.FileName).ToLower();
                        if (ext == ".txt" || ext == ".md")
                            File.WriteAllText(saveFileDialog.FileName, _editor.Text);
                        else
                            _editor.SaveFile(saveFileDialog.FileName);
                        OpenedDocumentPath = saveFileDialog.FileName;
                        IsOpened = true;
                        IsUnsaved = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving file: {ex.Message}");
                    }
                }
            };
        }

        public void MarkUnsaved() => IsUnsaved = true;
    }
}
