import {Component} from 'react';

class RemoveButtonClassComp extends Component{

   render(){
    return(
        <div>
            
            <button onClick={()=>this.props.removeTodo()}>Sil</button>

        </div>
    )
   }

}
export default RemoveButtonClassComp;