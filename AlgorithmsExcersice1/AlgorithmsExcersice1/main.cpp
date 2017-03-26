/*
Assigned by:
Daniel Gront #319301909
Mark Gront #319301883
*/

#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <list>
using namespace std;

/* Function declarations */

void Ex1();
void Ex2();

/* Declarations of other functions */

struct maxNumber
{
	//maxNumber(int stackSize) : stack_size((stackSize / 2) - 1), number_stack(new int[stack_size]), count(0) {}
	maxNumber(int number, int stackSize) : number(number), stack_size(stackSize / 2), number_stack(new int[stack_size]), count(0) {}
	//~maxNumber() { delete number_stack; }
	void push(int number);
	int pop();
	int number;
	bool isEmpty() const { return count == 0; }

private:
	int stack_size;
	int *number_stack;
	int count;
};

typedef list<maxNumber>::iterator listIterator;

void playOff(list<maxNumber> &myVector);
void pushStackAndErase(list<maxNumber> &myList, listIterator &beginIt, listIterator &nextIt);
int findMaxInStack(list <maxNumber> &myList);

/* ------------------------------- */

int main() 
{
	int select = 0, i, all_Ex_in_loop = 0;

	cout << "Run menu once or cyclically?\n(Once - enter 0, cyclically - enter other number)  ";

	if (scanf("%d", &all_Ex_in_loop) == 1)
		do {
			for (i = 1; i < 3; i++)
				cout << "Ex" << i << "--->" << i << endl;
			cout << "EXIT-->0" << endl;
			do 
			{
				select = 0;
				cout << "(please select 0-2 :) ";
				cin >> select;
			} while ((select < 0) || (select > 2));

			switch (select)
			{
				case 1: Ex1(); break;
				case 2: Ex2(); break;
			}
		} while (all_Ex_in_loop && select);

		return 0;
}


void Ex1()
{
	list<maxNumber> myList;
	int size;

	cout << "Please enter the amount of the players: ";
	cin >> size;

	for (int i = 0; i < size; i++)
	{
		int number;
		cin >> number;
		myList.push_back(maxNumber(number, size));
	}
	playOff(myList);

	int max2 = findMaxInStack(myList);
	cout << "The maximum is: " << myList.begin()->number << " The second maximum is: " << max2 << "\n\n";
}

int findMaxInStack(list <maxNumber> &myList)
{
	listIterator it = myList.begin();
	int localMax = it->pop();
	while (!it->isEmpty())
	{
		int tempMax = it->pop();
		if (localMax < tempMax)
			localMax = tempMax;
		
	}
	return localMax;
}

void playOff(list<maxNumber> &myList)
{
	while (myList.size() > 1)
	{
		size_t length = myList.size();
		listIterator beginIt = myList.begin();
		listIterator nextIt = ++(myList.begin());

		for (size_t i = 0; i < length / 2; ++i, ++beginIt)
		{
			if (beginIt->number > nextIt->number)
				pushStackAndErase(myList, beginIt, nextIt);
			else
				pushStackAndErase(myList, nextIt, beginIt);

			++nextIt;

			if (i < length / 2 - 1)
				++nextIt;
		}
	}
}

void pushStackAndErase(list<maxNumber> &myList, listIterator &beginIt, listIterator &nextIt)
{
	beginIt->push(nextIt->number);
	myList.erase(nextIt);
	nextIt = beginIt;
}

void Ex2()
{


	
}

void maxNumber::push(int number)
{
	if (count >= stack_size) 
		return;

	number_stack[count++] = number;
}

int maxNumber::pop()
{
	if (count == 0) return 0;
	return number_stack[--count];
}
