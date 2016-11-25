using UnityEngine;
using System.Collections;

public class InvalidOperandException : UnityException {

	public InvalidOperandException() : base("String was not 'Add' or 'Multiply' or 'Divide'")
    {

    }
}
