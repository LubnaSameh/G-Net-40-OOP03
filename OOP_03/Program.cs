namespace OOP_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Part 01: Theoretical Questions

                #region Q1: Relationship Types
                /*
                 * a) Composition
                 * b) Association
                 * c) Inheritance
                 * d) Aggregation
                 * e) Dependency
                 */
                #endregion

                #region Q2: Access Modifiers and Sealed
                /*
                 * * a) 
                 * - Yes, a child class in a different assembly can access a protected field.
                 * - No, you cannot access it through an object instance from the outside.
                 * * b) 
                 * - Protected Internal: Means the member is accessible in the same project or in any child class even in other projects.
                 * - Private Protected: Means the member is accessible only by child classes that are in the same project.
                 * * c) 
                 * - Sealed Class: It means no other class can inherit from this class.
                 * - Sealed Method: It means a child class cannot override this specific method.
                 * * d) 
                 * - Yes, you can create an object from a sealed class using new.
                 * - Why: Because 'sealed' only prevents other classes from inheriting from it
                     it does not stop us from using the class to make objects.
                 */
                #endregion

            #endregion
        }
    }
}