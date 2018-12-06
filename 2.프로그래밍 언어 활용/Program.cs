using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static string[] hMap = new string[10]; // 글작성 시 저장 되는 배열
        static int hIndex = 0;                 // 글작성 시 사용할 위치값(인덱스)
        static string[] pattern = { " ", "┌", "┐", "└", "┘", "─", "│", "┬", "┴", "┼", "┤", "├" }; // 영역 표현 기호 배열

        static void Main(string[] args)
        {
            /*********************************************************************
             * 다음 프로그램 완성하시오.
             * 조건1) 제어문과 반복문를 사용하여 구성 할것.
             * 조건2) System.Console.ReadKey() 를 이용하여 입력값을 받아 올것.
             * 조건3) 입력 받은 값을 이용하여 출력 할 내용을 변경 할것.
             * 조건4) 주어진 배열의 값을 이용하여 출력 방법을 정의 할것.
             *********************************************************************/
            MainBackground();     // 메인 배경화면 출력 함수

            int menuIndex = 0;    // 메뉴 선택 시 사용할 위치값(인덱스)
            MenuEvent(menuIndex); // 메뉴 화면 출력 함수

            while (true)
            {
                if (hIndex == hMap.Length) CloseEvent(); // 글작성 배열에 끝까지 추가 되었을때 프로그램 종료

                switch (Console.ReadKey().Key) // key 입력값에 따라 이벤트 처리 부분
                {
                    case ConsoleKey.UpArrow: // 위쪽 방향
                        menuIndex--;
                        break;
                    case ConsoleKey.DownArrow: // 아래쪽 방향
                        menuIndex++;
                        break;
                    case ConsoleKey.Enter: // 메뉴 실행 이벤트
                        DrowEvent(menuIndex);
                        break;
                    default: // 나머지 키가 입력 되었을때 다시 key 입력 받도록 처리
                        continue;
                }

                // 메뉴 부분 시각화 처리 및 예외 처리 부분
                if (menuIndex >= 3) menuIndex = 0;
                else if (menuIndex < 0) menuIndex = 2;
                MenuEvent(menuIndex);
            }
        }

        static void MainBackground() // 기본 화면 출력하기
        {  /********************************************************
            * 영역 표현 기호 내용
            * 0        1   2   3   4   5   6   7   8   9   10   11
            * 빈공간  ┌   ┐ └   ┘  ─  │  ┬  ┴  ┼  ┤   ├
            ********************************************************/
            Console.Clear(); // 콘솔 화면 초기화 하기
            int startX = 0;  // X축 시작점
            int startY = 0;  // Y축 시작점
            int[,] map = {
                            {1,5,5,5,5,7,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
                            {6,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {6,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {6,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {11,5,5,5,5,8,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,10},
                            {6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
                            {3,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,4}
                         }; // 화면 레이아웃 내용 배열

            Console.SetCursorPosition(startX, startY); // 시작점
            for (int i = 0; i < map.GetLength(0); i++) // 화면 출력 부분
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.SetCursorPosition((startX + (j * 2)), (startY + i));
                    Console.Write(pattern[map[i, j]]);
                }
            }
        }

        static void MenuEvent(int menuIndex) // 메뉴 출력하기
        {
            int startX = 2; // X축 시작점
            int startY = 1; // Y축 시작점
            string[] menus = { "시작", "목록", "종료" }; // 메뉴 내용 배열

            Console.SetCursorPosition(startX, startY);

            for (int y = 0; y < menus.Length; y++) // 메뉴 화면 출력
            {
                Console.SetCursorPosition(startX, (startY + y));
                if (menuIndex == y) Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write("  {0}  ", menus[y]);
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.SetCursorPosition(startX, startY + menuIndex);
        }

        static void DrowEvent(int menuIndex) // 메뉴 선택 시 동작 함수
        {
            switch (menuIndex)
            {
                case 0:
                    DrowStart(menuIndex); // 글작성 모드 실행
                    break;
                case 1:
                    ListPrint();          // 글작성 목록 출력 모드 실행
                    Update();
                    break;
                case 2:
                    CloseEvent();         // 프로그램 종료 모드 실행
                    break;
            }
        }


        static void ListPrint() // 글작성 배열 출력
        {
            // 문제1) hMap 배열에 들어 있는 내용을 아래 공간에 출력하는 하기.
            int index = 0;
            for (int i = 0; i < hMap.Length; i++)
            {
                Console.SetCursorPosition(5, i + 7);
                if (index == i) Console.BackgroundColor = ConsoleColor.Blue;
                if (hMap[i] != null)
                {
                    Console.WriteLine("{0}. {1}", i + 1, hMap[i]);
                }

                Console.BackgroundColor = ConsoleColor.Black;
            }

        }

        static void Update()
        {
            // 문제2) 출력한 글작성 목록 중 수정이 가능 하도록 하는 기능 추가하기.
            int index = 0;

            Console.SetCursorPosition(5, 15);
            Console.Write("숫자입력 : ");
            index = int.Parse(Console.ReadLine());

            Console.SetCursorPosition(20, 1);

            for (int i = 0; i < hMap.Length; i++)
            {
                if (hMap[index] != null)
                {
                    if (i +1 == index)
                    {
                        Console.Write("수정 글 : {0}", hMap[i]);
                    }
                }
            }
        }

        static void DrowStart(int menuIndex) // 프로그램 동작 호출
        {
            Console.SetCursorPosition(15, 1);
            Console.Write("글자를 입력하세요 :  ");
            string input = Console.ReadLine();
            if (input == "end")
            {
                DrowClear(menuIndex);
            }
            else
            {
                Console.SetCursorPosition(36, 1);
                Console.Write("{0}", input); // 입력값 확인하기
                hMap[hIndex] = input; // 입력값 배열에 담기
                hIndex++;             // 다음 배열 인덱스 만들기

                Console.SetCursorPosition(15, 2);
                Console.Write("입력이 완료되었습니다.");
                System.Threading.Thread.Sleep(1000);
                DrowClear(menuIndex);
            }
        }

        static void DrowClear(int menuIndex) // 프로그램 동작 정지 호출
        {
            MainBackground();
            MenuEvent(menuIndex);
        }

        static void CloseEvent() // 프로그램 종료
        {
            Console.SetCursorPosition(0, 21);
            Environment.Exit(0); // 프로그램 종료 함수
        }
    }
}