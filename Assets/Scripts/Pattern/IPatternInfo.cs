using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public interface IPatternInfo
{
    public IEnumerator PatternExecute(float duration);
}
