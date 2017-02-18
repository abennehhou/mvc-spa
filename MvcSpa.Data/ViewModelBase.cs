using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcSpa.Data
{
    public abstract class ViewModelBase
    {
        /// <summary>
        /// Indicates if product details area is visible.
        /// </summary>
        public bool IsDetailAreaVisible { get; set; }

        /// <summary>
        /// Indicates if the list of products area is visible.
        /// </summary>
        public bool IsListAreaVisible { get; set; }

        /// <summary>
        /// Indicates if search product area is visible.
        /// </summary>
        public bool IsSearchAreaVisible { get; set; }

        /// <summary>
        /// Indicates if the model is valid.
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// List of additional validation errors.
        /// </summary>
        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }

        /// <summary>
        /// Type of event command
        /// </summary>
        public EventCommandType EventCommand { get; set; }

        /// <summary>
        /// Display mode, if any
        /// </summary>
        public DisplayMode? Mode { get; set; }

        /// <summary>
        /// Argument passed with the command.
        /// </summary>
        public string EventArgument { get; set; }

        public ViewModelBase()
        {
            ValidationErrors = new List<KeyValuePair<string, string>>();

            EventCommand = EventCommandType.List;
            EventArgument = null;
            SetListMode();
        }

        public virtual void HandleRequest()
        {
            switch (EventCommand)
            {
                case EventCommandType.List:
                case EventCommandType.Search:
                case EventCommandType.Cancel:
                    SetListMode();
                    Get();
                    break;
                case EventCommandType.ResetSearch:
                    SetListMode();
                    ResetSearch();
                    Get();
                    break;
                case EventCommandType.Add:
                    Add();
                    break;
                case EventCommandType.Save:
                    Save();
                    break;
                case EventCommandType.Edit:
                    Edit();
                    break;
                case EventCommandType.Delete:
                    Delete();
                    break;
                default:
                    break;
            }
        }

        protected virtual void SetListMode()
        {
            IsValid = true;
            IsSearchAreaVisible = true;
            IsListAreaVisible = true;
            IsDetailAreaVisible = false;
        }

        protected virtual void SetDetailsMode()
        {
            IsSearchAreaVisible = false;
            IsListAreaVisible = false;
            IsDetailAreaVisible = true;
        }

        protected virtual void Add()
        {
            Mode = DisplayMode.Add;
            SetDetailsMode();
        }

        protected virtual void Edit()
        {
            Mode = DisplayMode.Edit;
            SetDetailsMode();
        }

        protected virtual void Delete()
        {
            SetListMode();
            Mode = null;
        }

        protected virtual void Get()
        {
        }

        protected virtual void ResetSearch()
        {
        }

        protected virtual void Save()
        {
            IsValid = IsValid && !ValidationErrors.Any();

            if (IsValid)
            {
                SetListMode();
                Get();
            }
            else
            {
                SetDetailsMode();
            }
        }
    }
}
