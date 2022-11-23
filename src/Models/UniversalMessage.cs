namespace Ocluse.LiquidSnow.Core.Models
{
    ///<summary>
    /// A class that can be used to pack messages sharable between assemblies.
    ///</summary>
    /// <remarks>
    /// This class implements <see cref="BindableBase"/>.
    /// </remarks>
    public class UniversalMessage : BindableBase
    {
        #region Properties

        private string? _sender, _header, _content;

        /// <summary>
        /// The identifier of the sender.
        /// </summary>
        public virtual string? Sender
        {
            get { return _sender; }
            set { SetProperty(ref _sender, value); }
        }

        /// <summary>
        /// The message title.
        /// </summary>
        public virtual string? Header
        {
            get { return _header; }
            set { SetProperty(ref _header, value); }
        }

        /// <summary>
        /// The body of the message.
        /// </summary>
        public virtual string? Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        /// <summary>
        /// Updates properties of the message.
        /// </summary>
        /// <param name="source">The source to be copied</param>
        public virtual void Update(UniversalMessage source)
        {
            Sender = source.Sender;
            Header = source.Header;
            Content = source.Content;
        }

        /// <summary>
        /// Creates a new message and copies the contents from the <paramref name="source"/> message.
        /// </summary>
        /// <param name="source">The source to be duplicated</param>
        /// <returns></returns>
        public static UniversalMessage Duplicate(UniversalMessage source)
        {
            return new UniversalMessage
            {
                Sender = source.Sender,
                Header = source.Header,
                Content = source.Content
            };
        }

        #endregion
    }
}
