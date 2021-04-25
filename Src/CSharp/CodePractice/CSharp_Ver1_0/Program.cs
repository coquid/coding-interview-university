using System;
using System.Runtime.InteropServices;

namespace CSharp_Ver1_0
{
    /*  
     *  1. Macro : #if, #define, #undef, #warning, #error, #line, #region, #pragma
     *  2. local variable's scrope
     *  3. Literal has type. (e.g. 5.ToString() == "5" , "asd".ToUpper() == "ASD")
     *  4. Attribute. System.Attribute 참고.
     */

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Enum, AllowMultiple = true)]
    class AuthorAttribute : System.Attribute
    {
        string name;
        int version;
        public int Version
        {
            get { return version; }
            set { version = value; }
        }
        public AuthorAttribute(string name)
        {
            this.name = name;
        }
    }

    [Author("Coquid", Version = 1), Flags] // 이런식으로 지정 가능.
    enum SampleEnum
    {

    }

    /*
     * 5. Shift operator
     */

    public class ShiftOp
    {
        static public void ShiftPractice1()
        {
            int n = 38;
            int leftShiftResult = n << 2;
            int rightShiftResult = n >> 2;

            Console.WriteLine(leftShiftResult);
            Console.WriteLine(rightShiftResult);
        }

        static public void OperatorPriority()
        {
            if( true || false && false)
            {
                Console.WriteLine("&& operator has higher priority than ||");
            }
        }


    }

    /*
     * 5.1.3 예약어 (Reserved word)
     * - checked, unchecked
     * - unsafe : unsafe context 영역 및 pointer 사용을 의미
     * - fixed : 힙에 할당된 메모리영역을 GC가 마음대로 바꾸지 못하게 막는 의미. 
     */

    public class ReservedWord
    {
        static public void CheckedUnChecked()
        {
            short c = short.MaxValue;

            unchecked
            {
                c++;
            }

            checked
            {
                c++;
            }
        }

        static public void NoParams(object[] objects)
        {
            foreach (var i in objects)
            {
                Console.WriteLine(i);
            }
        }
        static public void ParamsWriteLine(params object[] value)
        {
            foreach (var i in value)
            {
                Console.WriteLine(i);
            }
        }


        unsafe static void GetAddResult(int* p, int a, int b)
        {
            *p = a + b;
        }
        static public void UnsafeKeyword()
        {
            int i;
            unsafe
            {
                GetAddResult(&i, 5, 10);
            }

            Console.WriteLine(i);
        }

        class ManagedDummyData
        {
            public int count;
            public string name;
        }
        public unsafe static void FixedKeyword()
        {
            ManagedDummyData inst = new ManagedDummyData();
            inst.count = 5; inst.name = "text";

            // inst 객체 자체의 ptr를 직접 가져올 수는 없지만, 값 형식의 맴버는 가져올 수 있음.
            // 맴버 하나만 fixed 로 가져와도 객체 자체를 fixed 하는 효과가 있음. fixed 블록이 끝날 때 까지는 GC에 의해서 해당 객체 위치가 옮겨지지 않는다.
            fixed (int* pValue = &inst.count)
            {
                *pValue = 6;
            }
            fixed (char* pName = inst.name.ToCharArray())
            {
                for (int i = 0; i < inst.name.Length; i++)
                {
                    Console.WriteLine(*(pName + i));
                }
            }
        }

        struct CSharpStructType
        {
            public int[] intList;
            public long[] dummy;
        }

        unsafe struct FixedCSharpStructType
        {
            public fixed int intList[2];
            public fixed long dummy[3];
        }

        public void FixedStruct()
        {
            // 이렇게 c# struct 를 구현하면, int, long array이기 때문에 heap에 추가로 메모리할당해서, struct지만, 연속적이지 않은 메모리 레이아웃을 가짐.
            CSharpStructType cSharpStructType;
            cSharpStructType.intList = new int[2];
            cSharpStructType.dummy = new long[3];

            // fixed 키워드를 통해서, 고정된 struct size를 가져올 수 있어서, 메모리 레이아웃을 연속적으로 가져올 수 있음.
            FixedCSharpStructType fixedCSharpStructType;
        }

        /*
         * statckalloc keyword 
         * - 값형식이더라도 배열이면 Heap에 할당되는데, 이를 stack에 명시적으로 alloc 시킬수있는 키워드.
         * - 다만, unsafe 내에서 사용해야함.
         * - GC를 피할 수 있어서 좋음.
         * - 다만, 1MB 스택밖에 못쓰니까 특별한 경우 제외하고는 보통 쓰지 않는다.
         */
        static public unsafe void StackAlloc()
        {
            int* pArray = stackalloc int[1024];
        }

        /*
         * Todo
         * - volatile, lock : Thread와 함께 사용됨. 6장에서 계속...
         * - internal : 프로젝트 구성에서 계속...
         * - try, catch, throw, finally : 예외처리에서 계속...
         * - using : 지원 해제에서 계속...
         */
    }

    /*
     * Call Win32 API : extern keyword
     * - extern keyword : 코드가 없어도 컴파일 되도록하는 키워드일 뿐.
     * - 실제 Win32 API와 C# 코드의 연결은 DllImport Attribute 가 해준다.
     * - !!! pinvoke.net 웹사이트 참고하자. !!!
     */
    public class ExternKeyword
    {
        [DllImport("user32.dll")]
        static extern int MessageBeep(uint uType);

        static public int TestMethod(uint uType)
        {
            MessageBeep(0);

            return 0;
        }
    }

    [type : Author("Coquid", Version = 1)]
    [type : Author("Coquid2", Version = 2)]
    class Program
    {
        [method : Author("Coquid", Version = 1)]
        static void Main(string[] args)
        {

            // ShiftOp.ShiftPractice1();
            // ReservedWord.CheckedUnChecked();
            //object[] objectList = {1, 2,3,"s" };
            //ReservedWord.NoParams(objectList);
            //ReservedWord.ParamsWriteLine(1, 2, "3", 4, 5, "s");
            //ReservedWord.UnsafeKeyword();
            //ReservedWord.FixedKeyword();
        }
    }
}
