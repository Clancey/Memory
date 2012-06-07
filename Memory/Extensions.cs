// 
//  Copyright 2012  James Clancey, Xamarin Inc  (http://www.xamarin.com)
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using System;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace Memory
{
	public static class Extensions
	{
		
		public static void Shuffle<T> (this IList<T> list)
		{
			RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider ();
			int n = list.Count;
			while (n > 1) {
				byte[] box = new byte[1];
				do
					provider.GetBytes (box); while (!(box[0] < n * (Byte.MaxValue / n)));
				int k = (box [0] % n);
				n--;
				T value = list [k];
				list [k] = list [n];
				list [n] = value;
			}
		}
	
	}
}

