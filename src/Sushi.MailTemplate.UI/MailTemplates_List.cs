using System.Collections.Generic;
using Wim.Framework;

namespace Sushi.MailTemplate.UI
{
    /// <summary>
    /// MailTemplates_List Component List
    /// </summary>
    public class MailTemplates_List : ComponentListTemplate
    {
        /// <summary>
        /// MailTemplates_List ctor
        /// </summary>
        public MailTemplates_List()
        {
            wim.OpenInEditMode = false;
            wim.CurrentList.Option_AfterSaveListView = false;

            ListSearch += MailTemplatesList_ListSearch;
            ListLoad += MailTemplatesList_ListLoad;
            ListAction += MailTemplatesList_ListAction;
            ListPreRender += MailTemplates_List_ListPreRender;
            ListSave += MailTemplatesList_ListSave;
        }

        private void MailTemplates_List_ListPreRender(IComponentListTemplate sender, ComponentListEventArgs e)
        {
            if (wim.IsSaveMode)
            {
                if (!CanSave())
                {
                    wim.IsSaveMode = false;
                }
            }
        }
        private bool CanSave()
        {
            if (Implement == null) Implement = new Data.MailTemplate();
            Wim.Utility.ReflectProperty(this, Implement, true);

            if (string.IsNullOrWhiteSpace(Implement.Identifier))
            {
                wim.Notification.AddError("Mail template Identifier is required");

                return false;
            }

            //check there are no other templates with the same identifier
            var otherMailTemplate = Data.MailTemplateList.FetchSingleByIdentifier(Implement.Identifier);

            if (otherMailTemplate != null && otherMailTemplate.ID != Implement.ID)
            {
                wim.Notification.AddError($"Mail template {Implement.Identifier} is already in use by {otherMailTemplate.Name}.");
                return false;
            }

            if (!Logic.Helper.IsValid(Body))
            {
                wim.Notification.AddError("Body contains illegal placeholder characters. Only alphanumeric characters are allowed.");
            }

            if (!Logic.Helper.IsValid(Subject))
            {
                wim.Notification.AddError("Subject contains illegal placeholder characters. Only alphanumeric characters are allowed.");
            }

            return true;
        }

        private void MailTemplatesList_ListSave(IComponentListTemplate sender, ComponentListEventArgs e)
        {

            var currentTemplateInDatabase = Data.MailTemplateList.FetchSingle(Implement.ID);
            var result = Data.MailTemplateList.FetchSingleByIdentifier(Identifier);

            if (result != null && result.ID != Implement.ID)
            {
                Wim.Data.Notification.InsertOne("Wim.Module.MailTemplate", $"Identifier {Identifier} is already in use.");
                return;
            }

            var id = Implement.Save(wim.CurrentApplicationUser.ID, wim.CurrentApplicationUser.Displayname, wim.CurrentApplicationUser.Email);

            Response.Redirect(wim.GetCurrentQueryUrl(true, new KeyValue[] {
                        new KeyValue { Key = "list", Value =  wim.CurrentList.ID},
                        new KeyValue { Key = "item", Value = id }
            }));
        }

        private void MailTemplatesList_ListAction(object sender, ComponentActionEventArgs e)
        {
            if (BtnDeleteSelected)
            {
                if (wim.ChangedSearchGridItem != null)
                {
                    var mailTemplateIDs = new List<int>();

                    foreach (var item in wim.ChangedSearchGridItem)
                    {
                        var mailTemplate = item as Data.MailTemplateList;
                        mailTemplateIDs.Add(mailTemplate.ID);
                    }

                    var isSuccess = Data.MailTemplate.Delete(mailTemplateIDs);

                    if (!isSuccess)
                    {
                        wim.Notification.AddError("Failed to delete the selected templates.");
                    }

                    var keyValues = new List<Wim.Framework.KeyValue>();

                    Context.Response.Redirect(wim.GetCurrentQueryUrl(true, keyValues.ToArray()));
                }
            }

            if (BtnPublish)
            {
                if (CanSave())
                {
                    var result = Data.MailTemplateList.FetchSingleByIdentifier(Identifier);


                    if (result != null && result.ID != Implement.ID)
                    {
                        Wim.Data.Notification.InsertOne("Wim.Module.MailTemplate", $"Identifier {Identifier} is already in use.");
                        return;
                    }

                    if (Implement.Publish(wim.CurrentApplicationUser.ID, wim.CurrentApplicationUser.Displayname, wim.CurrentApplicationUser.Email))
                    {
                        wim.CurrentVisitor.Data.Apply("wim.note", "Template is published");
                        wim.CurrentVisitor.Save();

                        Response.Redirect(wim.GetCurrentQueryUrl(true, new KeyValue[] {
                        new KeyValue { Key = "list", Value =  wim.CurrentList.ID},
                        new KeyValue { Key = "item", Value = Implement.ID }
                    }));
                    }
                }
            }

            if (BtnDefaultValues)
            {

            }

            if (BtnRevert)
            {
                if (Implement.Revert(wim.CurrentApplicationUser.ID, wim.CurrentApplicationUser.Displayname, wim.CurrentApplicationUser.Email))
                {
                    wim.Notification.AddNotificationAlert($"Template has been reverted to the published version", true);

                    Response.Redirect(wim.GetCurrentQueryUrl(true, new KeyValue[] {
                        new KeyValue { Key = "list", Value =  wim.CurrentList.ID},
                        new KeyValue { Key = "item", Value = Implement.ID }
                    }));
                }
            }
        }

        /// <summary>
        /// Delete button to delete selected mail templates
        /// </summary>
        [Wim.Framework.ContentListSearchItem.Button("Delete selected", IconTarget = Wim.Framework.ButtonTarget.TopLeft, AskConfirmation = true, ConfirmationQuestion = "Are you sure you want to delete these templates?")]
        public bool BtnDeleteSelected { get; set; }

        /// <summary>
        /// The current mail template
        /// </summary>
        public Data.MailTemplate Implement { get; set; }

        private void MailTemplatesList_ListLoad(IComponentListTemplate sender, ComponentListEventArgs e)
        {
            Implement = Data.MailTemplate.FetchSingle(e.SelectedKey);
            if (Implement != null)
            {
                var latestTemplate = Data.MailTemplateList.FetchSingleByIdentifier(Implement.Identifier);

                if (latestTemplate != null && Implement.ID != latestTemplate.ID)
                {
                    // someone is using the url to go to an old template, redirect to newest
                    Response.Redirect(wim.GetCurrentQueryUrl(true, new KeyValue[] {
                        new KeyValue { Key = "list", Value =  wim.CurrentList.ID},
                        new KeyValue { Key = "item", Value = latestTemplate.ID }
                    }));
                }

                if (e.SelectedKey > 0)
                {
                    ListOfAvailablePlaceholders = "<b>Subject:</b><br/>";
                    ListOfAvailablePlaceholders += string.Join("<br/>", Logic.PlaceholderLogic.GetPlaceholderTags(Implement.Subject));
                    ListOfAvailablePlaceholders += "<br/><br/><b>Body:</b><br/>";
                    ListOfAvailablePlaceholders += string.Join("<br/>", Logic.PlaceholderLogic.GetPlaceholderTags(Implement.Body));

                }
            }
            else
            {
                Implement = new Data.MailTemplate();
            }

            // only show option to revert if there is a published version, a.k.a. a major version
            wim.SetPropertyVisibility(nameof(BtnRevert), Implement.VersionMinor > 0 && Implement.VersionMajor > 0);
            // only show publish button if current version is not published
            wim.SetPropertyVisibility(nameof(BtnPublish), !(Implement.IsPublished.HasValue && Implement.IsPublished.Value));
            wim.SetPropertyVisibility(nameof(BtnPreview), !wim.IsEditMode);
            wim.SetPropertyVisibility(nameof(BtnSendTestMail), !wim.IsEditMode);
            wim.SetPropertyVisibility(nameof(BtnDefaultValues), !wim.IsEditMode);

            Wim.Utility.ReflectProperty(Implement, this);
        }

        private void MailTemplatesList_ListSearch(object sender, ComponentListSearchEventArgs e)
        {
            // Don't need this in the export
            if (!wim.IsExportMode_XLS)
            {
                wim.ListDataColumns.Add(new ListDataColumn("Name", nameof(Data.MailTemplateList._IsSelected), ListDataColumnType.HighlightPresent)
                {
                    Alignment = Align.Left,
                    Type = ListDataColumnType.Checkbox,
                    ColumnWidth = 45,
                    EditConfiguration = new ListDataEditConfiguration()
                    {
                        Type = ListDataEditConfigurationType.Checkbox,
                        PropertyToSet = "_IsSelected",
                    }
                });
            }

            wim.ListDataColumns.Add(new ListDataColumn("ID", nameof(Data.MailTemplate.ID), ListDataColumnType.UniqueIdentifier));
            wim.ListDataColumns.Add(new ListDataColumn("Name", nameof(Data.MailTemplate.Name)));
            wim.ListDataColumns.Add(new ListDataColumn("Identifier", nameof(Data.MailTemplate.Identifier)));
            wim.ListDataColumns.Add(new ListDataColumn("Has published version", nameof(Data.MailTemplate.HasPublishedVersion)));
            wim.ListDataColumns.Add(new ListDataColumn("Is current published", nameof(Data.MailTemplate.IsPublished)));

            var items = Data.MailTemplateList.FetchAll(FilterText);
            wim.ListDataApply(items);
        }
        /// <summary>
        /// Button to open the default values
        /// </summary>
        [Wim.Framework.ContentListItem.Button("Default values", IconTarget = Wim.Framework.ButtonTarget.TopLeft, OpenInPopupLayer = true, CustomUrlProperty = "DefaultValuesUrl")]
        public bool BtnDefaultValues { get; set; }
        /// <summary>
        /// Url of the dialog with default values
        /// </summary>
        public string DefaultValuesUrl
        {
            get
            {
                var list = Wim.Data.ComponentList.SelectOne(typeof(DefaultValues_List));
                return Wim.Utility.AddApplicationPath($@"repository/wim/portal.ashx?list={list.ID}&item={Implement.ID}&openinframe=1");
            }
        }
        /// <summary>
        /// Publish button
        /// </summary>
        [Wim.Framework.ContentListItem.Button("Publish", IsPrimary = true)]
        public bool BtnPublish { get; set; }
        /// <summary>
        /// Revert button
        /// </summary>
        [Wim.Framework.ContentListItem.Button("Revert", AskConfirmation = true, ConfirmationQuestion = "Are you sure you want to revert to the published version?")]
        public bool BtnRevert { get; set; }
        /// <summary>
        /// Preview button
        /// </summary>
        [Wim.Framework.ContentListItem.Button("Preview e-mail", OpenInPopupLayer = true, CustomUrlProperty = "MessagePreview")]
        public bool BtnPreview { get; set; }
        /// <summary>
        /// Send test mail button
        /// </summary>
        [Wim.Framework.ContentListItem.Button("Send test e-mail"
            , IconTarget = Wim.Framework.ButtonTarget.TopRight
            , CustomUrlProperty = nameof(SendMailOpenUrl)
            , ListInPopupLayer = nameof(SendMailOpenGuid)
            , PopupLayerScrollBar = true
            , PopupLayerHeight = "400px")]
        public bool BtnSendTestMail { get; set; }
        /// <summary>
        /// Guid to the SendTestMail_List
        /// </summary>
        public static string SendMailOpenGuid
        {
            get
            {
                var list = Wim.Data.ComponentList.SelectOne(type: typeof(SendTestMail_List))?.GUID;

                return list.HasValue ? list.Value.ToString() : string.Empty;
            }
        }
        /// <summary>
        /// Url of the message preview dialog
        /// </summary>
        public string MessagePreview
        {
            get
            {
                var list = Wim.Data.ComponentList.SelectOne(typeof(ShowMailPreview_List));
                return Wim.Utility.AddApplicationPath($@"repository/wim/portal.ashx?list={list.ID}&MailTemplateID={Implement.ID}&state=1&openinframe=1");
            }
        }
        /// <summary>
        /// Url of the SendTestMail_List
        /// </summary>
        public string SendMailOpenUrl
        {
            get
            {
                int? list = Wim.Data.ComponentList.SelectOne(type: typeof(SendTestMail_List))?.ID;

                return wim.GetCurrentQueryUrl(true
                    , new Wim.Framework.KeyValue[] {
                        new Wim.Framework.KeyValue { Key = "list",  Value = list.HasValue ? list.Value : 0 },
                        new Wim.Framework.KeyValue { Key = "importtype", Value = "open" },
                        new Wim.Framework.KeyValue { Key = "openinframe", Value = "1" },
                    }
                    );
            }
        }
        /// <summary>
        /// Search textbox
        /// </summary>
        [Wim.Framework.ContentListSearchItem.TextField("Search", 50)]
        public string FilterText { get; set; }
        /// <summary>
        /// Name textbox
        /// </summary>
        [Wim.Framework.ContentListItem.TextField("Name", 50)]
        public string Name { get; set; }
        /// <summary>
        /// Description textfield
        /// </summary>
        [Wim.Framework.ContentListItem.TextField("Description", 255, IsRequired = false, Mandatory = false)]
        public string Description { get; set; }
        /// <summary>
        /// Identifier textbox
        /// </summary>
        [Wim.Framework.ContentListItem.TextField("Identifier", 50, Mandatory = true, IsRequired = true)]
        public string Identifier { get; set; }
        /// <summary>
        /// Default sender e-mail textbox
        /// </summary>
        [Wim.Framework.ContentListItem.TextField("Default sender email", 255)]
        public string DefaultSenderEmail { get; set; }
        /// <summary>
        /// Default sender name textbox
        /// </summary>
        [Wim.Framework.ContentListItem.TextField("Default sender name", 50)]
        public string DefaultSenderName { get; set; }
        /// <summary>
        /// BCC receivers textbox, ";"-separated string
        /// </summary>
        [Wim.Framework.ContentListItem.TextField("BCC receivers", 4000)]
        public string BCCReceivers { get; set; }
        /// <summary>
        /// Subject textbox
        /// </summary>
        [Wim.Framework.ContentListItem.TextField("Subject", 512, IsRequired = true, Mandatory = true)]
        public string Subject { get; set; }
        /// <summary>
        /// Body textbox
        /// </summary>
        [Wim.Framework.ContentListItem.TextArea("Body", 0, IsSourceCode = true, Mandatory = true)]
        public string Body { get; set; }
        /// <summary>
        /// List of available placeholders
        /// </summary>
        [Wim.Framework.ContentListItem.TextLine("List of available placeholders")]
        public string ListOfAvailablePlaceholders { get; set; }
    }
}
