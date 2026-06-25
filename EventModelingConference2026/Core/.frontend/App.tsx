// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { useTheme } from '../Utils/useTheme';
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { LayoutProvider } from '../Layout/Default/context/LayoutContext';
import { DialogComponents } from '@cratis/arc.react/dialogs';
import { BusyIndicatorDialog, ConfirmationDialog } from '@cratis/components/Dialogs';
import { DefaultLayout } from '../Layout/Default/DefaultLayout';
import { IMenuItemGroup } from '../Layout/Default/Sidebar/MenuItem/MenuItem';
import { Home } from '../Home';
import { Employees } from '../Employment/Employees/Employees';
import * as mdIcons from 'react-icons/md';

function App() {
    useTheme();

    const menuItems: IMenuItemGroup[] = [
        {
            items: [
                {
                    label: 'Employees',
                    url: '/employees',
                    icon: mdIcons.MdOutlinePeople,
                }
            ]
        }
    ];

    return (
        <LayoutProvider>
            <DialogComponents confirmation={ConfirmationDialog} busyIndicator={BusyIndicatorDialog}>
                <BrowserRouter>
                    <Routes>
                        <Route element={<DefaultLayout menu={menuItems} />}>
                            <Route path='/' element={<Home />} />
                            <Route path='/employees' element={<Employees />} />
                        </Route>
                    </Routes>
                </BrowserRouter>
            </DialogComponents>
        </LayoutProvider>
    );
}

export default App;
