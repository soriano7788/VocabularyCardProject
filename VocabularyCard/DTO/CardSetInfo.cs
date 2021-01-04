using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VocabularyCard.Validation;
using System.Web.Mvc;

namespace VocabularyCard.DTO
{
    [CustomValidation(typeof(CardSetValidator), "IsValid")]
    [Bind(Exclude= "Flag,CreatedDateTime,ModifiedDateTime")]  // Exclude 內的欄位表示不要被 view 傳進 controller action 時被自動 binding，
    public class CardSetInfo
    {
        private int _id;
        [ScaffoldColumn(false)]  // 此屬性不會被 scaffold 用來產生 view 相關程式碼 
        [Key]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _flag;
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

        private string _displayName;
        // 單字集名稱 的長度需介於 1 到 255
        [StringLength(255, MinimumLength = 1,ErrorMessage = "{0} 的長度需介於 {2} 到 {1}")]
        [DisplayName("單字集名稱")]
        [Required(ErrorMessage = "不得為空")]
        // 只能是大小寫英文字元 介於 1~20 個字之內
        [RegularExpression(@"^[a-zA-Z' '-'\s]{1,20}$")]
        [CustomValidation(typeof(DisplayNameValidator), "IsNotEmpty")]
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private DateTime _createdDateTime;
        [Range(typeof(DateTime), "1900/1/1", "9999/12/31")]
        public DateTime CreatedDateTime
        {
            get { return _createdDateTime; }
            set { _createdDateTime = value; }
        }

        private DateTime _modifiedDateTime;
        public DateTime ModifiedDateTime
        {
            get { return _modifiedDateTime; }
            set { _modifiedDateTime = value; }
        }

        private string _creator;
        public string Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }

        private string _modifier;
        public string Modifier
        {
            get { return _modifier; }
            set { _modifier = value; }
        }

        private string _owner;
        public string Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        private CardSetState _state;
        //[EnumDataType]
        public CardSetState State
        {
            get { return _state; }
            set { _state = value; }
        }

        private CardInfo[] _cards;
        public CardInfo[] Cards
        {
            get { return _cards; }
            set { _cards = value; }
        }
    }
}
