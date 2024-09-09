
import { AccessTime } from "@mui/icons-material";
import { createSlice, PayloadAction } from "@reduxjs/toolkit"

export interface IModalShow{
    show: boolean
}

export interface IUser {
    id?: number;
    firstName?: string;
    lastName?: string;
    email?: string;
} 

export interface IModal {
    IsAuthenticated: boolean
    userData: IUser
}

const initialState2: IModalShow = {
    show: true
}

 const initialState: IModal = {
    IsAuthenticated: true,
    userData: {
        id: 0, firstName: '', lastName: '', email: ''
    }
}


const modalSlice = createSlice({
    name: 'modal',
    initialState,
    reducers: {
        setIsAuthenticated: (state, action: PayloadAction<IModal>): void => {
            state.IsAuthenticated = action.payload.IsAuthenticated;
        },
        setUserData: (state, action: PayloadAction<IModal>): void => {
            state.userData = action.payload.userData;
            state.IsAuthenticated = action.payload.IsAuthenticated;
        }
    }
});


export const {setUserData} = modalSlice.actions;
export default modalSlice;