// ConsoleApplication1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>

int main()
{
	int i{ 10 };
	i = static_cast<int>(3.9);
	i = 4.5;
	std::cout << i << std::endl;
    return 0;
}

