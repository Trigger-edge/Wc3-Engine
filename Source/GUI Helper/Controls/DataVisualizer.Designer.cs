namespace Wc3Engine
{
    partial class DataVisualizer
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.ContentPanel = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.Accordion = new Opulos.Core.UI.Accordion();
            this.ContentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.AutoScroll = true;
            this.ContentPanel.AutoSize = true;
            this.ContentPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ContentPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ContentPanel.Controls.Add(this.splitContainer);
            this.ContentPanel.Controls.Add(this.Accordion);
            this.ContentPanel.Location = new System.Drawing.Point(0, 0);
            this.ContentPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(237, 142);
            this.ContentPanel.TabIndex = 63;
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer.Location = new System.Drawing.Point(16, 3);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Size = new System.Drawing.Size(221, 139);
            this.splitContainer.SplitterWidth = 1;
            this.splitContainer.TabIndex = 64;
            // 
            // Accordion
            // 
            this.Accordion.AddResizeBars = false;
            this.Accordion.AllowMouseResize = false;
            this.Accordion.AnimateCloseEffect = ((Opulos.Core.UI.AnimateWindowFlags)(((Opulos.Core.UI.AnimateWindowFlags.VerticalNegative | Opulos.Core.UI.AnimateWindowFlags.Hide) 
            | Opulos.Core.UI.AnimateWindowFlags.Slide)));
            this.Accordion.AnimateCloseMillis = 0;
            this.Accordion.AnimateOpenEffect = ((Opulos.Core.UI.AnimateWindowFlags)(((Opulos.Core.UI.AnimateWindowFlags.VerticalPositive | Opulos.Core.UI.AnimateWindowFlags.Show) 
            | Opulos.Core.UI.AnimateWindowFlags.Slide)));
            this.Accordion.AnimateOpenMillis = 0;
            this.Accordion.AutoFixDockStyle = true;
            this.Accordion.AutoScroll = true;
            this.Accordion.AutoSize = true;
            this.Accordion.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Accordion.BackColor = System.Drawing.SystemColors.Control;
            this.Accordion.CheckBoxFactory = null;
            this.Accordion.CheckBoxMargin = new System.Windows.Forms.Padding(0);
            this.Accordion.ContentBackColor = null;
            this.Accordion.ContentMargin = null;
            this.Accordion.ContentPadding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.Accordion.ControlBackColor = null;
            this.Accordion.ControlMinimumHeightIsItsPreferredHeight = true;
            this.Accordion.ControlMinimumWidthIsItsPreferredWidth = true;
            this.Accordion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Accordion.DownArrow = null;
            this.Accordion.FillHeight = true;
            this.Accordion.FillLastOpened = false;
            this.Accordion.FillModeGrowOnly = false;
            this.Accordion.FillResetOnCollapse = false;
            this.Accordion.FillWidth = true;
            this.Accordion.GrabCursor = System.Windows.Forms.Cursors.SizeNS;
            this.Accordion.GrabRequiresPositiveFillWeight = true;
            this.Accordion.GrabWidth = 6;
            this.Accordion.GrowAndShrink = true;
            this.Accordion.Insets = new System.Windows.Forms.Padding(0);
            this.Accordion.Location = new System.Drawing.Point(0, 0);
            this.Accordion.Name = "Accordion";
            this.Accordion.OpenOnAdd = false;
            this.Accordion.OpenOneOnly = false;
            this.Accordion.ResizeBarFactory = null;
            this.Accordion.ResizeBarsAlign = 0.5D;
            this.Accordion.ResizeBarsArrowKeyDelta = 10;
            this.Accordion.ResizeBarsFadeInMillis = 800;
            this.Accordion.ResizeBarsFadeOutMillis = 800;
            this.Accordion.ResizeBarsFadeProximity = 24;
            this.Accordion.ResizeBarsFill = 1D;
            this.Accordion.ResizeBarsKeepFocusAfterMouseDrag = false;
            this.Accordion.ResizeBarsKeepFocusIfControlOutOfView = true;
            this.Accordion.ResizeBarsKeepFocusOnClick = true;
            this.Accordion.ResizeBarsMargin = null;
            this.Accordion.ResizeBarsMinimumLength = 50;
            this.Accordion.ResizeBarsStayInViewOnArrowKey = true;
            this.Accordion.ResizeBarsStayInViewOnMouseDrag = true;
            this.Accordion.ResizeBarsStayVisibleIfFocused = true;
            this.Accordion.ResizeBarsTabStop = true;
            this.Accordion.ShowPartiallyVisibleResizeBars = false;
            this.Accordion.ShowToolMenu = true;
            this.Accordion.ShowToolMenuOnHoverWhenClosed = false;
            this.Accordion.ShowToolMenuOnRightClick = true;
            this.Accordion.ShowToolMenuRequiresPositiveFillWeight = false;
            this.Accordion.Size = new System.Drawing.Size(237, 142);
            this.Accordion.TabIndex = 0;
            this.Accordion.UpArrow = null;
            // 
            // DataVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.ContentPanel);
            this.Name = "DataVisualizer";
            this.Size = new System.Drawing.Size(237, 142);
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ContentPanel;
        private Opulos.Core.UI.Accordion Accordion;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}
