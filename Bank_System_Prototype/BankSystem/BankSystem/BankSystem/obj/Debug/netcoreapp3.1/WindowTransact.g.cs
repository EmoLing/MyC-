﻿#pragma checksum "..\..\..\WindowTransact.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "54DE95E9CF58CA1415CD5045BBF475A26AC1D07D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Bank_System;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Bank_System {
    
    
    /// <summary>
    /// WindowTransact
    /// </summary>
    public partial class WindowTransact : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\WindowTransact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboDepartment;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\WindowTransact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem ItemNatural;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\WindowTransact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem ItemLegal;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\WindowTransact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem ItemVIP;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\WindowTransact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox BoxAccountNumber;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\WindowTransact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LabelName;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\WindowTransact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButTransact;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\WindowTransact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButCancel;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\WindowTransact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButSearch;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\WindowTransact.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox BoxSum;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BankSystem;component/windowtransact.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WindowTransact.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\WindowTransact.xaml"
            ((Bank_System.WindowTransact)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\WindowTransact.xaml"
            ((Bank_System.WindowTransact)(target)).Unloaded += new System.Windows.RoutedEventHandler(this.Window_Unloaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ComboDepartment = ((System.Windows.Controls.ComboBox)(target));
            
            #line 11 "..\..\..\WindowTransact.xaml"
            this.ComboDepartment.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboDepartment_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ItemNatural = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 4:
            this.ItemLegal = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 5:
            this.ItemVIP = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 6:
            this.BoxAccountNumber = ((System.Windows.Controls.TextBox)(target));
            
            #line 17 "..\..\..\WindowTransact.xaml"
            this.BoxAccountNumber.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.BoxAccountNumber_TextChanged);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\WindowTransact.xaml"
            this.BoxAccountNumber.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.BoxAccountNumber_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 7:
            this.LabelName = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.ButTransact = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\WindowTransact.xaml"
            this.ButTransact.Click += new System.Windows.RoutedEventHandler(this.ButTransact_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ButCancel = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\WindowTransact.xaml"
            this.ButCancel.Click += new System.Windows.RoutedEventHandler(this.ButCancel_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ButSearch = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\WindowTransact.xaml"
            this.ButSearch.Click += new System.Windows.RoutedEventHandler(this.ButSearch_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.BoxSum = ((System.Windows.Controls.TextBox)(target));
            
            #line 23 "..\..\..\WindowTransact.xaml"
            this.BoxSum.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.BoxSum_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

