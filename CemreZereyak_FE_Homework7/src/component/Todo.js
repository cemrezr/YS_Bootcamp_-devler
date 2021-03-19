import {useState} from 'react';
import RemoveButtonClassComp from './RemoveButtonClassComp';

let Todo = () =>
{
    const [todo, setTodo] = useState("");
    const [todos, setTodos ]  = useState([]);

    let addTodo = ()=>{
        todos.push(todo);
        let tempArray = [...todos];
        setTodos(tempArray);
    }

    let removeTodo = (index) => {
        let todosTemp = [];
        todosTemp = [...todos];
        todosTemp.splice(index, 1);
        setTodos(todosTemp);
      };

    return(
        <div>
            <input type='text' onChange={(e)=> setTodo(e.target.value)}/>


        
            <button onClick={()=> addTodo()}>Kaydet</button>



            <ul>{todos.map((item, index )=> <li key={index}>
                {item} <RemoveButtonClassComp removeTodo={(index)=>removeTodo(index)}/>
            </li>)}</ul>

            
        </div>
    )


}
 export default Todo;