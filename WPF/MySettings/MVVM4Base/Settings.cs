using System.Collections.Generic;
using System.Configuration;

namespace MVVM4Base.Properties {

    public class Person
    {
        public string Name { get; set; } = "山本";

        public int Age { get; set; } = 18;

        public bool IsSend { get; set; } = true;

        public static readonly Person Default = new Person();

        public Person()
        {
        }
    }

    // このクラスでは設定クラスでの特定のイベントを処理することができます:
    //  SettingChanging イベントは、設定値が変更される前に発生します。
    //  PropertyChanged イベントは、設定値が変更された後に発生します。
    //  SettingsLoaded イベントは、設定値が読み込まれた後に発生します。
    //  SettingsSaving イベントは、設定値が保存される前に発生します。
    internal sealed partial class Settings {
        
        public Settings() {
            // // 設定の保存と変更のイベント ハンドラーを追加するには、以下の行のコメントを解除します:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // SettingChangingEvent イベントを処理するコードをここに追加してください。
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // SettingsSaving イベントを処理するコードをここに追加してください。
        }

        [UserScopedSetting()]
        public List<Person> People
        {
            get
            {
                return (List<Person>)this[nameof(People)];
            }
            set
            {
                this[nameof(People)] = value;
            }
        }
    }
}
