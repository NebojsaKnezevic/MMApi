// import { useState } from "react";
// import { LoginModal } from "../navbar/login/login-modal";
import './modal.css'
import { useSelector } from "react-redux";
import { RootState } from "../../store/store";
// import modalSlice from "../../store/login-modal/login-modal";





export interface IModalProps {
    // modalContent?: React.ReactElement,
    id?: string,
    header?: React.ReactElement,
    body?: React.ReactElement,
    footer?: React.ReactElement
}


export const CustomModal: React.FunctionComponent<IModalProps> = props => {
    const { id, header, body, footer } = props

    // const [showModalPopup, setShowModalPopup] = useState<boolean>(false);
    const showModalPopup = useSelector((state: RootState) => !state.loginModal.IsAuthenticated);
    // const handleToggleModalPopup = (x:boolean): void => {
    //     setShowModalPopup(x)
    // }

    return (<>
        <button
            className="btn btn-primary me-3 d-none"
            // onClick={()=>handleToggleModalPopup(true)}
        >Login</button>
        {showModalPopup &&
             <div id={id || 'Modal'} className='modall '>
             <div className='modal-content w-auto border border-2 shadow-sm rounded'>
                <div className='header'>
                    {/* <span className='close-modal-icon' onClick={()=>handleToggleModalPopup(false)}>&times;</span> */}
                    <h3>{header ? header : ''}</h3>
                </div>
                <div className='body'>
                    {body ? body : <div><p>Modal body</p></div>}
                </div>
                <div className='footer'>
                    {footer ? footer : <></>}
                </div>
             </div>
            </div>
        }
    </>);
}