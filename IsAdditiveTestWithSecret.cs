/*
Problem Statement Snippet (from email): 
You are given a function 'secret()' that accepts a single integer parameter and returns an integer. In your favorite programming language, write a command-line program that takes one command-line argument (a number) and determines if the secret() function is additive [secret(x+y) = secret(x) + secret(y)], for all combinations x and y, where x and y are all prime numbers less than the number passed via the command-line argument
*/

/*
Assumptions: 
For the purpose of the execution of our IsAdditive method, we have made the following assumptions
1) Input will be Integer value
2) Secret() method returns the output value same as the input value

Examples: 
1) Input: a
Output: Not a valid number, input must be numeric
2) Input: -1
Output: Number is not additive
3) Input: 1
Output: Number is not additive
4) Input: 2
Output: Number is additive
5) Input: 5
Output: Number is additive
*/

using System;
using System.Collections.Generic;

public class Program
{
	public static void Main()
	{
		//Taking input from user 
		string number = Console.ReadLine();
		int n;
		
		if(!Int32.TryParse(number, out n))
		{
			Console.WriteLine("Not a valid number, input must be numeric");
			return;
		}
		
		//Check is number is additive
		if(IsAdditive(n))
			Console.WriteLine("Number is additive");
		else
			Console.WriteLine("Number is not additive");
	}
	
	///<summary>
	///This function take an integer input, and returns true, if it is additive, for all X,Y 
	///such that X and Y are prime numbers less than given number. 
	///Definition of additive: secret(x+y) = secret(x) + secret(y)]
	///</summary>
	public static bool IsAdditive(int n)
	{
		int secretX, secretY, secretXPlusY;
		
		//If number passed is less than 2, then it cannot be additive as 2 is the smallest prime number
		if(n<2)
		{
			return false; 
		}
		
		//Get list of all primes numbers less than the given number 
		List<int> primes = GetPrimes(n);
		
		//Maintaining a dictionary to store a number as Key, and its secret() as Value
		//So that we can avoid executing secret for same value multiple times
		Dictionary<int,int> secretDict = new Dictionary<int, int>();
		
		//Iterate through each prime number (X)
		for(int i=0;i<primes.Count;i++)
		{
			secretX = GetSecretValue(primes[i], secretDict);
			
			//Iterate through all prime number including X, to get Y
			for(int j=0;j<=i;j++)
			{
			 	secretY = GetSecretValue(primes[j], secretDict);
			 	secretXPlusY = GetSecretValue(primes[i]+primes[j], secretDict);
			 
			 	//Check if pair is Additive per definition	
			 	if( secretXPlusY != (secretX + secretY))
			 	{
			 		return false; 
			 	}
			}
		}
		
		return true; 	
	}
	
	///<summary>
	///Gets the list of all prime numbers less than a given number
	///</summary>
	public static List<int> GetPrimes(int n)
	{
		var primes = new List<int>();
		
		for(int i=2; i<n; i++)
		{
			if(IsPrime(i))	
			{
				primes.Add(i);
			}
		}
		
		return primes; 
	}
	
	///<summary>
	///IsPrime() returns true if the given number is prime
	///</summary>
	public static bool IsPrime(int n)
	{
		//Edge case for 2 and 3, as prime
		if(n==2 || n==3) return true;
		
		//If a number is less than 2 or divisible by 2 or 3, it cannot be prime
		if(n<2 || n%2==0 || n%3==0) return false;
		
		//Starting loop with 5, as all other cases are already handled above
		//Check if the number is divisible by any number from 5 to its Sqrt
		for(int i=5; i <= Math.Sqrt(n); i+=2)
		{
			if(n%i==0)	return false; 
		}
		
		return true;
	}
	
	///<summary>
	///GetSecretValue() tries to get the evaluated secret for a given value from dictionary and return it
	///If the secret is not evaluated, then it evaluates the secret and returns the value
	///</summary>
	public static int GetSecretValue(int n, Dictionary<int,int> secretDict)
	{
		int secretN;
		if(secretDict.ContainsKey(n))
		{ 
			secretN = secretDict[n];
		}
		else
		{
			secretN = Secret(n);
			secretDict.Add(n, secretN);
		}
		return secretN;
	}

	///<summary>
	///Dummy secret method (which should be defined) for the purpose of executing this program, 
	///Secret() method returns the output value same as the input value
	///</summary>
	public static int Secret(int n)
	{
		return n;
	}
}