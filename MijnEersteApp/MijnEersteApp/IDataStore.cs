using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MijnEersteApp
{
	public interface IDataStore<T>
	{
		//CRUD operation
		Task<bool> CreateItem(T item); //POST
		Task<T> ReadItem(); //GET
		Task<bool> UpdateItem(T item); //PUT
		Task<bool> DeleteItem(T item); //DELETE
	}
}
