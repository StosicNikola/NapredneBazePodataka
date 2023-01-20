import axios from "axios";
import { useState } from "react";



function RegionCard({ regionName, regionId }) {
    const [mountains, setMountains] = useState([])
    const [mountainTops, setMountainTops] = useState([])
    const [showMountains, setShowMountains] = useState(false)
    const [showMountainsTops, setShowMountainsTops] = useState(false)

    const returnMountainTopInRegion = (regionid) => {
        axios.get(`https://localhost:5001/MountainTop/region/${regionId}`)
            .then(res => {
                setMountainTops(res.data[0])
                setMountainTops(true)
            })
            .catch(e => alert(e))
    }

    const returnMountainInRegion = () => {
        axios.get(`https://localhost:5001/Mountain/region/${regionId}`)
            .then(res => {
                setMountains(res.data[0])

                setShowMountains(true)
            })
            .catch(e => alert(e))
    }

    return (<><div className="region-name">
        <h5>{regionName}</h5>
        <button onClick={returnMountainInRegion}>Mountains</button>
        <button onClick={returnMountainTopInRegion}>Mountain Tops</button>
        {showMountains &&
            mountains.map(element => {
                return <div className="region-card">
                    <p>Name:{element.name} </p>
                    <p>Surface: {element.surface}</p>
                </div>
            })
        }
        {showMountainsTops &&
            mountainTops.map(element => {
                return <div>
                    <p>Name:{element.name} </p>
                    <p>Surface: {element.height}</p>
                </div>
            })
        }
    </div></>)
}
export default RegionCard;