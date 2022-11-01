using System.Windows;
using System.Windows.Data;

namespace TheXamlGuy.UI.WPF;

public class ConditionExtension : CompositeExtension
{
    private readonly Binding conditionBinding;

    private static readonly DependencyProperty ConditionProperty =
        DependencyProperty.RegisterAttached("Condition",
            typeof(bool), typeof(ConditionExtension));

    public ConditionExtension(Binding conditionBinding, 
        object args0) : base(args0)
    {
        this.conditionBinding = conditionBinding;
    }

    public ConditionExtension(Binding conditionBinding, 
        object args0, 
        object args1) : base(args0, args1)
    {
        this.conditionBinding = conditionBinding;
    }

    public ConditionExtension(Binding conditionBinding, 
        object args0, 
        object args1, 
        object args2) : base(args0, args1, args2)
    {
        this.conditionBinding = conditionBinding;
    }

    public ConditionExtension(Binding conditionBinding, 
        object args0, 
        object args1, 
        object args2, 
        object args3) : base(args0, args1, args2, args3)
    {
        this.conditionBinding = conditionBinding;
    }

    public ConditionExtension(Binding conditionBinding,
        object args0, 
        object args1, 
        object args2, 
        object args3, 
        object args4) : base(args0, args1, args2, args3, args4)
    {
        this.conditionBinding = conditionBinding;
    }

    public ConditionExtension(Binding conditionBinding, 
        object args0, 
        object args1, 
        object args2, 
        object args3,
        object args4, 
        object args5) : base(args0, args1, args2, args3, args4, args5)
    {
        this.conditionBinding = conditionBinding;
    }

    public ConditionExtension(Binding conditionBinding, 
        object args0, 
        object args1, 
        object args2, 
        object args3, 
        object args4, 
        object args5, 
        object args6) : base(args0, args1, args2, args3, args4, args5, args6)
    {
        this.conditionBinding = conditionBinding;
    }

    public ConditionExtension(Binding conditionBinding, 
        object args0, 
        object args1, 
        object args2, 
        object args3, 
        object args4, 
        object args5, 
        object args6, 
        object args7) : base(args0, args1, args2, args3, args4, args5, args6, args7)
    {
        this.conditionBinding = conditionBinding;
    }

    public ConditionExtension(Binding conditionBinding,
        object args0, 
        object args1, 
        object args2, 
        object args3, 
        object args4, 
        object args5, 
        object args6,
        object args7, 
        object args8) : base(args0, args1, args2, args3, args4, args5, args6, args7, args8)
    {
        this.conditionBinding = conditionBinding;
    }

    public ConditionExtension(Binding conditionBinding, 
        object args0,
        object args1, 
        object args2, 
        object args3, 
        object args4, 
        object args5, 
        object args6, 
        object args7, 
        object args8, 
        object args9) : base(args0, args1, args2, args3, args4, args5, args6, args7, args8, args9)
    {
        this.conditionBinding = conditionBinding;
    }

    public ConditionExtension(Binding conditionBinding, 
        object args0, 
        object args1, 
        object args2, 
        object args3, 
        object args4, 
        object args5, 
        object args6, 
        object args7, 
        object args8, 
        object args9, 
        object args10) : base(args0, args1, args2, args3, args4, args5, args6, args7, args8, args9, args10)
    {
        this.conditionBinding = conditionBinding;
    }

    public ConditionExtension(Binding conditionBinding, 
        object args0, 
        object args1, 
        object args2, 
        object args3, 
        object args4, 
        object args5, 
        object args6, 
        object args7, 
        object args8, 
        object args9, 
        object args10, 
        object args11) : base(args0, args1, args2, args3, args4, args5, args6, args7, args8, args9, args10, args11)
    {
        this.conditionBinding = conditionBinding;
    }

    public ConditionExtension(Binding conditionBinding,
        object args0, 
        object args1, 
        object args2,
        object args3,
        object args4,
        object args5, 
        object args6,
        object args7, 
        object args8, 
        object args9, 
        object args10,
        object args11,
        object args12) : base(args0, args1, args2, args3, args4, args5, args6, args7, args8, args9, args10, args11, args12)
    {
        this.conditionBinding = conditionBinding;
    }

    public ConditionExtension(Binding conditionBinding,
        object args0, 
        object args1,
        object args2, 
        object args3,
        object args4,
        object args5, 
        object args6, 
        object args7,
        object args8,
        object args9, 
        object args10, 
        object args11, 
        object args12, 
        object args13) : base(args0, args1, args2, args3, args4, args5, args6, args7, args8, args9, args10, args11, args12, args13)
    {
        this.conditionBinding = conditionBinding;
    }

    public ConditionExtension(Binding conditionBinding, 
        object args0, 
        object args1,
        object args2, 
        object args3, 
        object args4, 
        object args5, 
        object args6,
        object args7,
        object args8, 
        object args9, 
        object args10,
        object args11,
        object args12, 
        object args13, 
        object args14) : base(args0, args1, args2, args3, args4, args5, args6, args7, args8, args9, args10, args11, args12, args13, args14)
    {
        this.conditionBinding = conditionBinding;
    }

    public ConditionExtension(Binding conditionBinding, 
        object args0,
        object args1, 
        object args2, 
        object args3, 
        object args4, 
        object args5, 
        object args6, 
        object args7, 
        object args8, 
        object args9, 
        object args10,
        object args11, 
        object args12, 
        object args13, 
        object args14, 
        object args15) : base(args0, args1, args2, args3, args4, args5, args6, args7, args8, args9, args10, args11, args12, args13, args14, args15)
    {
        this.conditionBinding = conditionBinding;
    }

    protected override void OnInvoked(object sender, EventArgs args)
    {
        BindingOperations.SetBinding(TargetObject, ConditionProperty, conditionBinding);
        if (TargetObject?.GetValue(ConditionProperty) is true)
        {
            base.OnInvoked(sender, args);
        }
    }
}