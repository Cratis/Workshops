// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { useLayoutContext } from '../context/LayoutContext';
import css from './TopBar.module.css';
import { FaBars } from 'react-icons/fa6';

export const TopBar = () => {
    const { toggleLeftSidebarOpen } = useLayoutContext();

    return (
        <div className={css.container}>
            <div className={`flex items-center justify-between ${css.leftSide}`}>
                <div className={css.sidebarToggle}>
                    <button
                        type="button"
                        aria-label="Toggle sidebar"
                        onClick={toggleLeftSidebarOpen}
                        className={css.hamburgerMenuButton}>
                        <FaBars />
                    </button>
                </div>
                <div className="flex-1 flex align-center justify-center">
                    <div className="font-extrabold text-2xl m-2">Library</div>
                </div>
            </div>
            <div className="flex-1 flex items-center justify-end px-5 gap-6">
                <div>
                </div>
            </div>
        </div>
    );
};
