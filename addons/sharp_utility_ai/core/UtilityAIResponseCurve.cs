using Godot;
using System;

[Tool]
[GlobalClass]
public partial class UtilityAIResponseCurve : Curve {
    public enum CurveType {
        BINARY,
        LINEAR,
        EXPONENTIAL,
        LOGISTIC,
        CUSTOM,
    }

    [Export]
    public CurveType Type {
        get => _type;
        set {
            SetCurveType(value);
        }
    }
    
    [Export(PropertyHint.Range, "1,100,")]
    public int Exponent {
        get => _exponent;
        set {
            _exponent = value;
            UpdatePoints();
        }
    }
    
    [Export(PropertyHint.Range, "1,100,")]
    public int Slope {
        get => _slope;
        set {
            _slope = value;
            UpdatePoints();
        }
    } 
    
    [Export(PropertyHint.Range, "-1,1,")]
    public float XShift {
        get => _xShift;
        set {
            _xShift = value;
            UpdatePoints();
        }
    }
    
    [Export(PropertyHint.Range, "-1,1,")]
    public float YShift {
        get => _yShift;
        set {
            _yShift = value;
            UpdatePoints();
        }
    }

    private CurveType _type = CurveType.LINEAR;
    private int _exponent = 1;
    private int _slope = 1;
    private float _xShift = 0f;
    private float _yShift = 0f;

    private bool _ignoreChanged = false;

    public UtilityAIResponseCurve() {
        Changed += onChanged;
    }

    public float Evaluate(float x) {
        return base.Sample(x);
    }

    private void SetCurveType(CurveType newCurveType) {
        _type = newCurveType;
        NotifyPropertyListChanged();

        Exponent = 1;
        Slope = 1;
        XShift = 0f;
        YShift = 0f;

        switch(Type) {
            case CurveType.EXPONENTIAL:
                Exponent = 2;
                break;
            case CurveType.LOGISTIC:
                Exponent = 10;
                break;
        }

        UpdatePoints();
    }

    private void AddPoints(Callable f, int nPoints = 10) {
        for(int i = 0; i <= nPoints; ++i) {
            var x = (float)i / nPoints;
            var y = (float)f.Call(x);
            AddPoint(new Vector2(x, y), 0, 0, TangentMode.Linear, TangentMode.Linear);
        }
    }

    private void UpdatePoints(int nPoints = 10) {
        if(Type == CurveType.CUSTOM)
            return;
        
        _ignoreChanged = true;

        ClearPoints();

        if(Type == CurveType.BINARY) {
            var threshold = Clamp(0.5f + XShift);
            var lo = Clamp(0f + YShift);
            var hi = Clamp(1f + YShift);

            if(threshold == 0f) {
                AddPoint(new Vector2(0, hi));
                AddPoint(new Vector2(1, hi));
            }
            else {
                AddPoint(new Vector2(0, lo));
                AddPoint(new Vector2(threshold, hi));
                AddPoint(new Vector2(threshold, lo));
                AddPoint(new Vector2(1, hi));
            }
        }
        else if(Type == CurveType.LINEAR) {
            Func<float, float> linear =
                x => Clamp(Slope * (x - XShift) + YShift);

            var linearCallable = Callable.From(linear);
            AddPoints(linearCallable, nPoints);
        }
        else if(Type == CurveType.EXPONENTIAL) {
            Func<float, float> exponential = 
                x => Clamp(Slope * Mathf.Pow(x - XShift, Exponent) + YShift);
            
            var exponentialCallable = Callable.From(exponential);
            AddPoints(exponentialCallable, nPoints);
        }
        else if(Type == CurveType.LOGISTIC) {
            Func<float, float> logistic =
               x => Clamp(Slope / (1.0f + Mathf.Exp(-Exponent * (x - 0.5f - XShift))) + YShift);

            var logisticCallable = Callable.From(logistic);
            AddPoints(logisticCallable, nPoints);
        }

        _ignoreChanged = false;
    }

    private void onChanged() {
        if(_ignoreChanged)
            return;
        if(Type != CurveType.CUSTOM)
            SetCurveType(CurveType.CUSTOM);
    }

    private float Clamp(float x) {
        return Mathf.Clamp(x, 0f, 1f);
    }
}